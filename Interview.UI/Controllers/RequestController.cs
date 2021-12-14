using Interview.Data.Entities.Concrete;
using Interview.UI.Models;
using InterviewService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.UI.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IRepository<Request> _requestRepository;
        public RequestController(IRepository<Request> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {

            var requests = _requestRepository.GetAll().Where(x => x.IsEvaluated == false && x.IsActive).Select(x => new RequestViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                IsEvaluated = x.IsEvaluated,
                AdminMessage = x.AdminMessage,
                Message = x.Message,
                FileStr = Convert.ToBase64String(x.File),
                UserId = x.UserId,
                CreatedDate = x.CreatedDate,
            }).ToList();

            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ListTheOld()
        {
            var requests = _requestRepository.GetAll().Where(x => x.IsEvaluated && x.IsActive).Select(x => new RequestViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                IsEvaluated = x.IsEvaluated,
                AdminMessage = x.AdminMessage,
                Message = x.Message,
                FileStr = Convert.ToBase64String(x.File),
                UserId = x.UserId,
                CreatedDate = x.CreatedDate, // null gelmiyor ama listTheOld.cshtml'de null oluyor 
                UpdatedDate = x.UpdatedDate,
                IsPositive = x.IsPositive,
                     
            }).ToList();

            
            return View(requests);
        }

        [Authorize(Roles = "User")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int currentUserId = GetCurrentUserId();

            Request entity = new Request()
            {
                Name = model.Name,
                Surname = model.Surname,
                Message = model.Message,
                UserId = currentUserId,
                CreatedById = currentUserId,
                
            };

            #region File

            if (model.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.File.CopyTo(ms);
                    var fileByteArray = ms.ToArray();

                    entity.File = fileByteArray;
                }
            }
            else
            {
                ViewBag.Message = "Boş dosya yükleyemezsiniz";
            }

            #endregion

            bool result;
            entity.CreatedDate = DateTime.Now;
            result = _requestRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("ListMyRequest");
            }

            ViewBag.Message = "Bir şeyler ters gitti!";
            return View("Add", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int id)
        {
            var request = _requestRepository.Get(x => x.Id == id && x.IsActive);

            var vm = new RequestViewModel() // vm'de File null olarak detail sayfasına gidiyor. (FileStr != null, UserId != null geliyor.)
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Message = request.Message,
                FileStr = Convert.ToBase64String(request.File),
                IsPositive = request.IsPositive,
                CreatedDate = request.CreatedDate,
                UserId = request.UserId,
                
                //File = request.File,

                // byte[] bytes = Encoding.ASCII.GetBytes(vm.FileStr);
                // entity.File = bytes;

            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Detail(RequestViewModel model)
        {
            

            //if (!ModelState.IsValid)
            //{
            //    return View(model); // file null gelince buraya düşüyor.
            //}

            var currentUserId = GetCurrentUserId();

            var entity = new Request()
            {
                Name = model.Name,
                Surname = model.Surname,
                AdminMessage = model.AdminMessage,
                Message = model.Message,
                UserId = model.UserId,               
                IsActive = true,
            };

            bool result;
            byte[] file = Encoding.ASCII.GetBytes(model.FileStr); //
            entity.File = file;
            entity.Id = model.Id;
            entity.UpdatedById = currentUserId;
            entity.UpdatedDate = DateTime.Now;
            entity.IsEvaluated = true;
            entity.IsPositive = model.IsPositive;

            result = _requestRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            ViewBag.Message = "Bir şeyler ters gitti!";
            return View(model);
        }

        [Authorize(Roles = "User")]
        public ActionResult ListMyRequest(int id)
        {
            id = GetCurrentUserId();

            var requests = _requestRepository.GetAll(x => x.UserId == id && x.IsActive).Select(x => new RequestViewModel()
            {
                Id = x.Id,
                Message = x.Message,
                FileStr = Convert.ToBase64String(x.File),
                AdminMessage = x.AdminMessage,
                IsPositive = x.IsPositive,
                UpdatedDate = x.UpdatedDate,
            }).ToList();

            return View(requests);
        }

    //    public ActionResult Download()
    //    {
    //        var document = ...;
    //        var cd = new System.Net.Mime.ContentDisposition
    //    {
    //    // for example foo.bak
    //    FileName = document.FileName,

    //    // always prompt the user for downloading, set to true if you want 
    //    // the browser to try to show the file inline
    //    Inline = false,
    //};
    //        Response.AppendHeader("Content-Disposition", cd.ToString());
    //        return File(document.Data, document.ContentType);
    //    }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int id)
        {
            var currentUserId = GetCurrentUserId();

            var request = _requestRepository.Get(x => x.Id == id && x.UserId == currentUserId);

            if (request != null)
            {
                var result = _requestRepository.Delete(id);
            } 

            TempData["Message"] = "Silme yapılamadı";

            return RedirectToAction("ListMyRequest");
        }

    }
}
