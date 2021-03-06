// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SaleManager.Core.Entities
{
    [Index(nameof(RegionID), Name = "IX_Territories_RegionID")]
    public partial class Territories
    {
        [Key]
        public int TerritoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }

        [ForeignKey(nameof(RegionID))]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; }
    }
}