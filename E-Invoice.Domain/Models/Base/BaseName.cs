using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domian.Models
{
   public class BaseName: Base
    {
        public string Name { get; set; }
        public virtual string Notes { get; set; }
    }
}
