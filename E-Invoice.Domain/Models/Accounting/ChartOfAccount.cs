using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models.Accounting
{
    class ChartOfAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool HasParent { get; set; }
        public AccountNature Nature { get; set; }
    }
}
