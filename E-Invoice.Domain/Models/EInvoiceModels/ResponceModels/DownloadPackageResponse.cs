using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invoice.Domain.Models
{
    public class DownloadPackageResponse
    {
        public byte[] zip { get; set; }
        public error error { get; set; }
    }
}
