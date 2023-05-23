using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Employe
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

        public int  DepartmentId { get; set; }
        public virtual Department Department { get; set; }  
        public string ImageUrl { get; set; }

    }
}
