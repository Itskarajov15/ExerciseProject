using Microsoft.AspNetCore.Mvc;

namespace ExerciseProject.API.Features.Contragents
{
    [ApiController]
    [Route("[controller]")]
    public class ContragentsController : ControllerBase
    {
        private readonly IContragentsService contragentsService;

        public ContragentsController(IContragentsService contragentsService)
        {
            this.contragentsService = contragentsService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(ContragentDto contragent)
        {
            var isCreated = await this.contragentsService.Create(contragent);

            return Ok(isCreated);
        }

        [HttpGet]
        [Route(nameof(GetAllByUserId))]
        public async Task<IActionResult> GetAllByUserId([FromQuery] int userId)
        {
            var contragents = await this.contragentsService.GetContragentsByUserId(userId);

            return Ok(contragents);
        }
    }
}