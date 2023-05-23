using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Interfaces
{
    public interface IGenericReposotry<T>
    {
        Task<T> Get(int? id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T obj);
        Task<int> Update(T obj);
        Task<int> Delete(T obj);
    }
}
