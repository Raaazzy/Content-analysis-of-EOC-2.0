using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Service.Interfaces;
using EOC_2_0.ViewModels;

namespace EOC_2_0.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVerbService _verbService;

        public HomeController(IVerbService verbService)
        {
            _verbService = verbService;
        }

        [HttpGet]
        public IActionResult IndexAsync()
        {
            var response = _verbService.GetVerbs();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = response.Data;
                return View(obj);
            }
            return RedirectToAction("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<ActionResult> GetVerb(int id)
        {
            var response = await _verbService.GetVerb(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = (IEnumerable<Data.Models.Verb>)response.Data;
                return View(obj);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _verbService.DeleteVerb(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return RedirectToAction("GetVerbs");
            }
            return RedirectToAction("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView();
            }

            var response = await _verbService.GetVerb(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = (IEnumerable<Data.Models.Verb>)response.Data;
                return View(obj);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(VerbViewModel model)
        {
            if (ModelState.IsValid)
            {
                _verbService.Save(model);
            }
            return RedirectToAction("GetVerbs");
        }

        [HttpPost]
        public async Task<IActionResult> GetVerb(string term)
        {
            var response = await _verbService.GetVerb(term);
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetVerb(int id, bool isJson)
        {
            var response = await _verbService.GetVerb(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetVerb", response.Data);
        }

    }
}
