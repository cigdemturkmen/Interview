using Interview.Data.Entities.Concrete;
using Interview.UI.Models;
using InterviewService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var requests = _requestRepository.GetAll().Where(x => x.IsEvaluated == false).Select(x => new RequestViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                IsEvaluated = x.IsEvaluated,

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
                CreatedDate = DateTime.Now

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
            var requests = _requestRepository.Get(x => x.Id == id);

            var vm = new RequestViewModel()
            {
                Name = requests.Name,
                Surname = requests.Surname,
                Message = requests.Message,
                FileStr = Convert.ToBase64String(requests.File),
                IsPositive = requests.IsPositive,
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Detail(RegisterViewModel model)
        {
            return null;
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
            }).ToList();

            return View(requests);
           
        }




    }
}
