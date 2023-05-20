using ExerciseProject.API.Features.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseProject.API.Features.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(UserDto userModel)
        {
            var isCreated = await this.userService.Create(userModel);

            return Ok(isCreated);
        }

        [HttpGet]
        [Route(nameof(GetUsers))]
        public async Task<ActionResult> GetUsers()
        {
            var users = await this.userService.GetUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route(nameof(GetUserById))]
        public async Task<ActionResult> GetUserById(int id)
        {
            var users = await this.userService.GetUserById(id);

            return Ok(users);
        }

        [HttpPut]
        [Route(nameof(UpdateUser))]
        public async Task<ActionResult> UpdateUser(int id, UserDto userModel)
        {
            await this.userService.UpdateUser(id, userModel);

            return Ok();
        }

        [HttpPost]
        [Route(nameof(ValidateUser))]
        public async Task<ActionResult> ValidateUser(UserDto userModel)
        {
            var userIsValid = await this.userService.ValidateUser(userModel);

            return Ok(userIsValid);
        }
    }
}