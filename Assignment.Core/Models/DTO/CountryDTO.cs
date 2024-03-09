using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Models.DTO
{
    public class CountryDTO
    {
        public string CommonName { get; set; }
        public string Capital { get; set; }
        public string[] Borders { get; set; }
    }
}
