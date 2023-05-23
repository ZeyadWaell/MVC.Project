using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
	public class Department
    {
		public int Id { get; set; }
		public string Code { get; set; }
		[Required(ErrorMessage ="Department is required")]
		[MinLength(3,ErrorMessage = " MinLenth is 3 Charachters ")]
		public string Name { get; set; }
		public DateTime DateoffCreation { get; set; }

        public virtual ICollection<Employe> Employess { get; set; } = new List<Employe>();

	}
}
