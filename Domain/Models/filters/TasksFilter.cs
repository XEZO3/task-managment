using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.filters
{
    public class TasksFilter
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Completion { get; set; }
    }
}
