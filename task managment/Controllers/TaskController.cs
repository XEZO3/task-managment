using Domain.IService;
using Domain.Models;
using Domain.Models.filters;
using EComerce.ActionFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using task_managment.Common;

namespace task_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
       // [ServiceFilter(typeof(ValidationFilter))]
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("GetAll")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetAll([FromQuery]TasksFilter filter) {

           
            return Ok(await _taskService.GetAll(predicate:x=>(x.Title.Contains(filter.Title)||filter.Title==null)&&(x.Description.Contains(filter.Description)|| filter.Description==null)&&(x.Completion.Contains(filter.Completion)||filter.Completion==null)));
        }
        [HttpGet("getById/{Id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await _taskService.GetById(Id));
        }
        [HttpPut("Update")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        public  IActionResult Update(Tasks task)
        {
            return Ok( _taskService.Update(task));
        }
        [HttpDelete("Delete/{Id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Delete(int Id)
        {
            return Ok(await _taskService.DeleteById(Id));
        }
        [HttpPost("Add")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilter))]
        
        public async Task<IActionResult> Add(Tasks Task) {
            Task.UsersId = UserId.Id;
            return Ok(_taskService.Add(Task));
        }
    }
}
