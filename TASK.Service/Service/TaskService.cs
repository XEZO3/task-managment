using AutoMapper;
using Azure.Core;
using Domain.IRepository;
using Domain.IService;
using Domain.Models;
using Domain.Models.ServiceRespone;
using Domain.utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using task_managment.Common;

namespace TASK.Service.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private ServiceRespone<TasksRepone> TaskObj { get; set; }
        private ServiceRespone<IEnumerable<TasksRepone>> TaskList { get; set; }
        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            TaskObj = new ServiceRespone<TasksRepone>() { returnCode = Convert.ToString(codes.ok) };
            TaskList = new ServiceRespone<IEnumerable<TasksRepone>>() { returnCode = Convert.ToString(codes.ok) };
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceRespone<TasksRepone>> Add(Tasks entity)
        {
            var task = await _taskRepository.Add(entity);
            _unitOfWork.Save();
            TaskObj.result =_mapper.Map<TasksRepone>(task);
            return TaskObj;
        }

        public ServiceRespone<TasksRepone> Delete(Tasks entity)
        {
            if (_taskRepository.CanUserDo(entity.Id))
            {
                _taskRepository.Delete(entity);
                _unitOfWork.Save();
            }
            else {
                TaskObj.errorMsg = "you are not allowed to do this action";
            }
            return TaskObj;
        }

        public async Task<ServiceRespone<TasksRepone>> DeleteById(int Id)
        {
            if (_taskRepository.CanUserDo(Id))
            {
                var task = await _taskRepository.GetById(Id);
                _taskRepository.Delete(task);
                _unitOfWork.Save();
            
        }
            else {
                TaskObj.errorMsg = "you are not allowed to do this action";
            }
            return TaskObj;
        }

        public async Task<ServiceRespone<TasksRepone>> FirstOrDefult(Expression<Func<Tasks, bool>> predicate = null)
        {
            var task = await _taskRepository.FirstOrDefult(predicate);
            TaskObj.result = _mapper.Map<TasksRepone>(task);
            return TaskObj;
        }

        public async Task<ServiceRespone<IEnumerable<TasksRepone>>> GetAll(Expression<Func<Tasks, bool>> predicate = null)
        {
          
            var tasks = await _taskRepository.GetAll(predicate);
            var task = tasks.Where(x=>x.UsersId==UserId.Id);
            //task.Where(x=>x.UsersId== id);
            TaskList.result = _mapper.Map<IEnumerable<TasksRepone>>(task);
            return TaskList;
        }

        public async Task<ServiceRespone<TasksRepone>> GetById(int Id)
        {
            if (_taskRepository.CanUserDo(Id)) { 
            var task = await _taskRepository.GetById(Id);
            TaskObj.result = _mapper.Map<TasksRepone>(task);
            }
            else
            {
                TaskObj.errorMsg = "you are not allowed to do this action";
            }
            return TaskObj;
        }

        public ServiceRespone<TasksRepone> Update(Tasks entity)
        {
            if (_taskRepository.CanUserDo(entity.Id))
            {
                var task = _taskRepository.Update(entity);
                _unitOfWork.Save();
                TaskObj.result = _mapper.Map<TasksRepone>(task);
            }
            else
            {
                TaskObj.errorMsg = "you are not allowed to do this action";
            }
            return TaskObj;
        }

        public async Task<ServiceRespone<IEnumerable<TasksRepone>>> GetAll(int UserId, Expression<Func<Tasks, bool>> predicate = null)
        {
            var tasks = await _taskRepository.GetAll(predicate);
            var task=tasks.Where(x=>x.UsersId== UserId);
            TaskList.result = _mapper.Map<IEnumerable<TasksRepone>>(task);
            return TaskList;
        }
    }
}
