﻿using ExerciseProject.Core.Contracts;
using ExerciseProject.Core.Models.Contragents;
using ExerciseProject.Core.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContragentViewModel contragent)
        {
            if (!ModelState.IsValid)
            {
                return View(contragent);
            }

            contragent.UserId = int.Parse(User.Claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);

            var isCreated = await this.contragentsService.Create(contragent);

            if (!isCreated)
            {
                ModelState.AddModelError(String.Empty, "A contragent with that VAT number already exists.");
                return Json(new { success = false, issue = contragent, errors = ModelState.Values.Where(i => i.Errors.Count > 0) });
            }

            return Json(new { redirectToUrl = Url.Action("GetAll", "Contragents") });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.Claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);
            
            var contragents = await this.contragentsService.GetAllByUserId(userId);

            return View(contragents);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int contragentId)
        {
            var contragent = await this.contragentsService.GetContragentForEdit(contragentId);

            if (contragent.UserId == int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value))
            {
                return View(contragent);
            }

            return RedirectToAction("GetAll", "Contragents");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditContragentViewModel contragent)
        {
            if (contragent.UserId != int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value))
            {
                return RedirectToAction("GetAll", "Contragents");
            }

            if (!ModelState.IsValid)
            {
                return View(contragent);
            }

            await this.contragentsService.EditContragent(contragent);

            return RedirectToAction("GetAll", "Contragents");
        }
    }
}