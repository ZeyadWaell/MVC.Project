using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Interfaces
{
	public interface IDepartmentRepostory : IGenericReposotry<Department>
    {
		//Employe GetDepartment(int? id);
		//IEnumerable<Employe> GetAll();

		//int Add(Employe department);
		////int Update(Employe department);
		////int Delete(Employe department);
		//Task<Department> GetDepartmentByEmplyeeId(int? Id);
	}
}
