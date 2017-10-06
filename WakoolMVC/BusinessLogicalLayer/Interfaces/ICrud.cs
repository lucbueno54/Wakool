using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    interface ICrud<T>
    {
        List<object> GetAll(T typo, Type type);
        object GetById(T tipo, Type type);
        bool Delete(T tipo);
        bool Update(T obj);
        bool Insert(T obj);
    }
}
