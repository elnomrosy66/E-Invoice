using E_Invoice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Invoice.DAL.Repositories;
using System.Linq.Expressions;
using E_Invoice.Domian.Models;

namespace E_Invoice.Domain.Helpers
{
   
    public class SearchIEnum
    {
        public SearchIEnum()
        {
            FillCompo<Product>(x => true);
        }

        public static IEnumerable<T> FillCompo<T>(Expression<Func<T, bool>> Condetion) where T : class
        {
            //Cast<Base>().Where(c => c.IsDelete == IsDelete.Active)
          IUnitOfWork unitOfWork = new UnitOfWork();

          return  unitOfWork.GetData<T>().GetAllBy(Condetion).ToList();

        }
    }
}
