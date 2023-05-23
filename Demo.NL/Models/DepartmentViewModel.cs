using Demo.DAL.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.NL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Department is required")]
        [MinLength(3, ErrorMessage = " MinLenth is 3 Charachters ")]
        public string Name { get; set; }
        public DateTime DateoffCreation { get; set; }

        public virtual ICollection<EmployeModelView> Employess { get; set; } = new List<EmployeModelView>();

    }
}
