using Demo.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Repostoies
{
    public class UniteofWork : IUniteofWork
    {
        public UniteofWork(IEmplyeeRepostory eplyeeRepostory, IDepartmentRepostory dpartmentRepostory)
        {
            EmplyeeRepostory=eplyeeRepostory;
            DepartmentRepostory =dpartmentRepostory;
        }
        public IEmplyeeRepostory EmplyeeRepostory { get; set; }
        public IDepartmentRepostory DepartmentRepostory { get; set; }
    }
}
