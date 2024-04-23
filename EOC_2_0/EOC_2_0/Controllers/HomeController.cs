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

namespace EOC_2_0.Controllers
{
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
        [HttpPost]
        public async Task<ActionResult> IndexAsync(string str = null, int level = 1)
        {
            var response = _verbService.GetVerbs(level);
            var response2 = _nounService.GetNouns(1);
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
        public async Task<ActionResult> GetNoun(string str = null, string verb = null, int level = 0)
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
                return PartialView("NounsList", Tuple.Create(new List<Noun>(), level));
            }

            if (!string.IsNullOrEmpty(str))
            {
                var response = _nounService.GetNoun(str, verbId);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return PartialView("NounsList", Tuple.Create(response.Data, level));
                }
                return RedirectToAction("Error");
            }

            var response2 = _nounService.GetNouns(verbId);
            if (response2.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView("NounsList", response2.Data);
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
