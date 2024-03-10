using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Models.DTO
{
    /// <summary>
    /// Represents a DTO for country information.
    /// </summary>
    public class CountryDTO
    {
        /// <summary>
        /// Common name of the country.
        /// </summary>
        /// <example>Greece</example>
        [Required(AllowEmptyStrings = true)]
        public string CommonName { get; set; }

        /// <summary>
        /// Capital city of the country.
        /// </summary>
        /// <example>Athens</example>
        [Required(AllowEmptyStrings = true)]
        public string Capital { get; set; }

        /// <summary>
        /// Array of strings representing the borders of the country.
        /// </summary>
        /// <remarks>
        /// Each string in the array represents the common name of a country that shares a border with the current country.
        /// </remarks>
        /// <example>["Albania", "Bulgaria", "Turkey", "North Macedonia"]</example>
        [Required]
        public string[] Borders { get; set; }
    }
}
