using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatihanCRUDMVC.Model.Repository
{
    public interface IBaseRepository<T>
        where T : class
    {
        int Save(T obj);
        int Update(T obj);
        int Delete(T obj);
        List<T> GetAll();
    }
}
