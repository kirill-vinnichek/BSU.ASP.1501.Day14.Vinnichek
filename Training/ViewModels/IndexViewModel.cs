using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training.ViewModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalItems { get; set; } 
        public int TotalPages  
            => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }

    public class IndexViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Training.Models.Training> Trainings { get; set; }
    }
}