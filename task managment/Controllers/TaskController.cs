using Domain.IService;
using Domain.Models;
using Domain.Models.filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace task_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery]TasksFilter filter) {

            var token = Request.Headers.Authorization[0].Replace("Bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string Id = jwt.Claims.First(c => c.Type == "Id").Value;
            return Ok(await _taskService.GetAll(Convert.ToInt32(Id),predicate:x=>(x.Title.Contains(filter.Title)||filter.Title==null)&&(x.Description.Contains(filter.Description)|| filter.Description==null)));
        }
        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await _taskService.GetById(Id));
        }
        [HttpPut("Update")]
        public  IActionResult Update(Tasks task)
        {
            return Ok( _taskService.Update(task));
        }
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            return Ok(_taskService.DeleteById(Id));
        }
    }
}
