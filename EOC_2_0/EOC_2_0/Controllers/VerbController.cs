using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Service.Interfaces;
using EOC_2_0.ViewModels;
using Automarket.Service.Implementations;

namespace EOC_2_0.Controllers
{
    public class VerbController : Controller
    {
        private readonly IVerbService _verbService;

        public VerbController(IVerbService verbService)
        {
            _verbService = verbService;
        }

        [HttpGet]
        public IActionResult GetVerbs(int level = 1)
        {
            var response = _verbService.GetVerbs(level);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allVerbs = new List<IEnumerable<Verb>>();
                for (int i = 0; i < 6; i++)
                {
                    obj.allVerbs.Add(response.Data);
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
    }
}
