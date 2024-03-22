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
        private readonly INounService _nounService;

        public HomeController(IVerbService verbService, INounService nounService)
        {
            _verbService = verbService;
            _nounService = nounService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> IndexAsync(string str = null, int level = 1)
        {
            var response = _verbService.GetVerbs(level);
            var response2 = _nounService.GetNouns();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = new List<IEnumerable<Verb>>();
                for (int i = 0; i < 6; i++)
                {
                    obj.allVerbs.Add(response.Data.Where(x => x.Level == i + 1).ToList());
                }
                obj.allNouns = new List<IEnumerable<Noun>>();
                for (int i = 0; i < 6; i++)
                {
                    obj.allNouns.Add(response2.Data);
                }
                obj.inputText = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    obj.inputText.Add(null);
                }
                return View(obj);
            }
            return RedirectToAction("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<ActionResult> GetVerb(string str = null, int level = 1)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var response = _verbService.GetVerb(str, level);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return PartialView("VerbsList", response.Data);
                }
                return RedirectToAction("Error");
            }

            var response2 = _verbService.GetVerbs(level);
            if (response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView("VerbsList", response2.Data);
            }
            return RedirectToAction("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<ActionResult> GetNoun(string str = null)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var response = _nounService.GetNoun(str);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return PartialView("NounsList", response.Data);
                }
                return RedirectToAction("Error");
            }

            var response2 = _nounService.GetNouns();
            if (response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView("NounsList", response2.Data);
            }
            return RedirectToAction("Error", $"{response2.Description}");
        }
    }
}
