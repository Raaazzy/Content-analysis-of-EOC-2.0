using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Service.Interfaces;
using EOC_2_0.ViewModels;
using EOC_2_0.Data.Enum;
using EOC_2_0.Data.Response;
using System;
using Swashbuckle.Swagger;

namespace EOC_2_0.Controllers
{
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly IVerbService _verbService;
        private readonly INounService _nounService;
        private readonly INewVerbService _newVerbService;

        public HomeController(IVerbService verbService, INounService nounService, INewVerbService newVerbService)
        {
            _verbService = verbService;
            _nounService = nounService;
            _newVerbService = newVerbService;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            var response = _verbService.GetVerbs(1);
            var response2 = _nounService.GetNouns(1);
            if (response.StatusCode == Data.Enum.StatusCode.Success && response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = new List<IEnumerable<Verb>>();
                obj.allNouns = new List<IEnumerable<Noun>>();
                obj.inputText = new List<string>();
                for (int i = 0; i < 6; i++)
                {
                    obj.allVerbs.Add(response.Data.Where(x => x.Level == i + 1).ToList());
                    obj.allNouns.Add(response2.Data);
                    obj.inputText.Add(null);
                }
                return View(obj);
            }
            return RedirectToAction("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<ActionResult> GetVerb(string verb = null, string str = null, int level = 1)
        {
            if (!string.IsNullOrEmpty(str) && str.EndsWith(' '))
            {
                return PartialView("VerbsList", Tuple.Create(new List<Verb>(), level));
            }

            if (!string.IsNullOrEmpty(verb))
            {
                var response = _verbService.GetVerb(verb, level % 7);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return PartialView("VerbsList", Tuple.Create(response.Data, level));
                }
                return RedirectToAction("Error");
            }

            var response2 = _verbService.GetVerbs(level % 7);
            if (response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView("VerbsList", Tuple.Create(response2.Data, level));
            }
            return RedirectToAction("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<ActionResult> GetNoun(string noun = null, string verb = null, int level = 0)
        {
            int verbId = 1;
            var responseVerb = _verbService.GetVerb(verb, level % 7);
            if (responseVerb.StatusCode == Data.Enum.StatusCode.Success)
            {
                if (responseVerb.Data.Count != 1)
                {
                    return PartialView("NounsList", Tuple.Create(new List<Noun>(), level));
                }
                if (responseVerb.Data.Count == 1 && responseVerb.Data[0].Word != verb)
                {
                    return PartialView("NounsList", Tuple.Create(new List<Noun>(), level));
                }
                verbId = responseVerb.Data[0].Id;
            } else
            {
                return RedirectToAction("Error", $"{responseVerb.Description}");
            }

            if (!string.IsNullOrEmpty(noun))
            {
                var response = _nounService.GetNoun(noun, verbId);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return PartialView("NounsList", Tuple.Create(response.Data, level));
                }
                return RedirectToAction("Error");
            }

            var response2 = _nounService.GetNouns(verbId);
            if (response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView("NounsList", Tuple.Create(response2.Data, level));
            }
            return RedirectToAction("Error", $"{response2.Description}");
        }

        [HttpPost]
        public async Task<ActionResult> SaveVerb(string newVerb = null, int level = 1)
        {
            if (!string.IsNullOrEmpty(newVerb))
            {
                var responseVerb = _verbService.GetVerb(newVerb, level % 7);
                if (responseVerb.StatusCode == Data.Enum.StatusCode.Success)
                {
                    if (responseVerb.Data.Count == 0)
                    {
                        var response = await _newVerbService.Create(newVerb, level % 7);
                        if (response.StatusCode == Data.Enum.StatusCode.Success)
                        {
                            return View();
                        }
                        return RedirectToAction("Error");
                    }
                }
            }
            return View();
        }
    }
}
