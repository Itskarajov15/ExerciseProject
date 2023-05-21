using ExerciseProject.Core.Contracts;
using ExerciseProject.Core.Models.Contragents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExerciseProject.WEB.Controllers
{
    [Authorize]
    public class ContragentsController : Controller
    {
        private readonly IContragentsService contragentsService;

        public ContragentsController(IContragentsService contragentsService)
        {
            this.contragentsService = contragentsService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContragentViewModel contragent)
        {
            if (!ModelState.IsValid)
            {
                return View(contragent);
            }

            contragent.UserId = int.Parse(User.Claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);

            var isCreated = await this.contragentsService.Create(contragent);

            if (!isCreated)
            {
                ModelState.AddModelError(String.Empty, "Something went wrong!");
                return View(contragent);
            }

            return RedirectToAction("GetAll", "Contragents");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.Claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);
            
            var contragents = await this.contragentsService.GetAllByUserId(userId);

            return View(contragents);
        }
    }
}