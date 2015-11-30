using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training.Models;

namespace Training.ViewModels
{
    public class TrainingViewModel
    {
        public Models.Training Training { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}