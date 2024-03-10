using System.ComponentModel.DataAnnotations;

namespace Assignment.API.Models
{
    /// <summary>
    /// RequestObj class.
    /// </summary>
    public class RequestObj
    {
        /// <summary>
        /// Array of integers.
        /// </summary>
        /// <example>[5, 10, 15, 20]</example>
        [Required]
        public IEnumerable<int> RequestArrayObj { get; set; }
    }
}
