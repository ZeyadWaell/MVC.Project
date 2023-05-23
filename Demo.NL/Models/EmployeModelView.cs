using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.NL.Models
{
    public class EmployeModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int DepartmentId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Imafe { get; set; }
        public virtual DepartmentViewModel Department { get; set; }
    }
}
