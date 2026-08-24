using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Invoice.Domian.Models
{
   public class Store: BaseName
    {

      // [NotMapped]
      // public override string Notes { get => base.Notes; set => base.Notes=value; }

         private new string Notes { get => base.Notes; set => base.Notes = value; }



    }
}
