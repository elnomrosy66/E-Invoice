using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class setting : Base
    {

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TokenPass { get; set; }
        public bool ProdEnv { get; set; }
        public bool Signer { get; set; }
        public bool UseStoreBalance { get; set; }
        public bool UseGpc { get; set; }
    }
}
