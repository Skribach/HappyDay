using HappyDay.Models;
using HappyDay.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace HappyDay.Controllers
{
    [Authorize]
    [Controller]
    public class BirthdayController : Controller
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdayController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return View(null);
            }

            var model = _birthdayService.GetByUserId(userId);            

            return View(model);
        }

        public IActionResult AddOrEdit(BirthdayModel model)
        {
            //If user clicked "Edit"
            if (model.Id != null && model.BiDay == null)
            {
                var modelFromDb = _birthdayService.GetById(model.Id);
                return View(modelFromDb);
            }

            //Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Add
            if (model.Id == null)
            {
                model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _birthdayService.Add(model);
            }

            //Update
            else
            {
                _birthdayService.Edit(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string Id)
        {
            _birthdayService.DeleteById(Id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}