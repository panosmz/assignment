using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Core.Models
{
    public class Country
    {
        [Key, Column(TypeName = "CHAR(3)")]
        [StringLength(3)]
        public string Id { get; set; }
        public string CommonName { get; set; }
        public string Capital { get; set; }
        public string[] Borders { get; set; }
    }
}
