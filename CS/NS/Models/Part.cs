using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NS.Models
{
    public class Part {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Spec { get; set; }
        [Required]
        public string UOM { get; set; } //Unit of measurement
        [Column(TypeName = "decimal(18,2)")]


        public decimal UnitPrice { get; set; } //Unit of measurement
        public string SupplierCode { get; set; } //Unit of measurement
        [Required]
        public string Category { get; set; } //Unit of measurement
        public string Remarks { get; set; } //Unit of measurement



    }
}
