using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Invoice.Domain.Models;
namespace E_Invoice.Domain.Models
{
    public class DebitNoteModel
    {
        public int Inv_no { get; set; }
        public List<int> ser { get; set; }
    }
}