using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    /// <summary>
    /// salable product
    /// </summary>
    public class Product
    {
        [Key] // make primary key in database
        public int ProductId { get; set; }

        /// <summary>
        /// The consumer facing name of the product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The retail price as USD
        /// </summary>
        [DataType(DataType.Currency)]
        [Display(Name ="Retail Price")]
        public double Price { get; set; }

        /// <summary>
        /// Category product falls under. Ex. Electronics, Funitures, etc.
        /// </summary>
        public string Category { get; set; }

    }
}
