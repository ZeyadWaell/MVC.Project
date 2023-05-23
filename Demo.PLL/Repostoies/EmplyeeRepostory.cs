using Demo.DAL.Context;
using Demo.DAL.Entites;
using Demo.PLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Repostoies
{
    public class EmplyeeRepostory : GenericRepostory<Employe>, IEmplyeeRepostory
    {
          private readonly MVCAPPContext _context;
        public EmplyeeRepostory(MVCAPPContext context):base(context)
        {
            _context = context;
        }

        public async Task<string> GetDepartmentByEmplyeeId(int? Id)
        {
            var emplyee = await _context.Employes.Where(e => e.Id == Id).Include(e => e.Department).FirstOrDefaultAsync();
            var department = emplyee.Department;
            return department.Name;
        }
        //    public int Add(Employe employe)
        //    {
        //        _context.Employes.Add(employe);
        //        return _context.SaveChanges();
        //    }

        //    public int Delete(Employe employe)
        //    {
        //        _context.Employes.Remove(employe);
        //        return _context.SaveChanges();
        //    }

        //    public IEnumerable<Employe> GetAll()
        //           => _context.Employes.ToList();

        //    public Employe GetDepartment(int? id)
        //        => _context.Employes.FirstOrDefault(x => x.Id == id);

        //    public int Update(Employe employe)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        public async Task<IEnumerable<Employe>> GetEmplyessByDepaertmentNames(string DepName)
        => await _context.Employes.Where(e => e.Department.Name == DepName).ToListAsync();

        public async Task<IEnumerable<Employe>> Search(string name)
        => await _context.Employes.Where(x => x.Name == name).ToListAsync();
       
    }
    }
