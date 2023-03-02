using CV.DAL.Data;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Azure.Core;
using task_managment.Common;
using System.Security.Claims;

namespace EComerce.ActionFilter
{
    public class ValidationFilter : IActionFilter 
    {
        private readonly ITaskRepository _taskRepository;
        
        public ValidationFilter( ITaskRepository taskRepository)
        {
            _taskRepository= taskRepository;

            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
           
        }

        public  void OnActionExecuting(ActionExecutingContext context)
        {

            //var identity = context.HttpContext.User.Identity as ClaimsIdentity;

            var token = context.HttpContext.Request.Headers.Authorization[0].Replace("Bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string userid = jwt.Claims.First(c => c.Type == "Id").Value;
            UserId.Id = Convert.ToInt32(userid);



        }
    }
}
