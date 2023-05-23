using Demo.DAL.Context;
using Demo.DAL.Entites;
using Demo.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Repostoies
{
    public class DepRepostory : GenericRepostory<Department>, IDepartmentRepostory
    {
        private readonly MVCAPPContext _context;
        public DepRepostory(MVCAPPContext context) : base(context)
        {
            _context = context;
        }


    }
}
