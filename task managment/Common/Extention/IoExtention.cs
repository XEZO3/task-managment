

using Domain.IRepository;
using Domain.IService;
using EC.DAL.Repository;
using EC.Service.Service;
using Task.DAL.Repository;
using TASK.Service.Service;

namespace EComerce.Common.Extention
{
    public static class IoExtention
    {
        public static IServiceCollection RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            return builder.Services;
        }
    }
}
