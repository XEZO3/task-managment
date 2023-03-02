using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto
{
    public class TasksDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Completion { get; set; }
        
        public int? UsersId { get; set; }
    }
}
