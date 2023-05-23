using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Interfaces
{
    public interface IEmplyeeRepostory : IGenericReposotry<Employe>
    {
        //Employe GetDepartment(int? id);
        //IEnumerable<Employe> GetAll();

        //int Add(Employe department);
        //int Update(Employe department);
        //int Delete(Employe department);
        Task<string> GetDepartmentByEmplyeeId(int? Id);
        Task<IEnumerable<Employe>> GetEmplyessByDepaertmentNames(string DepName);
        Task<IEnumerable<Employe>> Search(string name);
    }
}
