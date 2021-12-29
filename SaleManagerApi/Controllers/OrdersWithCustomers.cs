using SaleManager.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesMan.Controllers
{
    public class OrdersWithCustomers
    {
        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        [StringLength(150)]
        public string OrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }
        [Column(TypeName = "money")]
        public decimal? GrandTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DiscountRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DiscountTotal { get; set; }
        public string ShipFees { get; set; }
        public string AssemblyFees { get; set; }
        public string PaymentType { get; set; }
        [StringLength(60)]
        public string ShipAddress { get; set; }
        [StringLength(15)]
        public string ShipCity { get; set; }
        [StringLength(15)]
        public string ShipRegion { get; set; }
        [StringLength(10)]
        public string ShipPostalCode { get; set; }
        [StringLength(15)]
        public string ShipCountry { get; set; }
        public bool? Approved { get; set; }



        [ForeignKey(nameof(ShipVia))]
        [InverseProperty(nameof(Shippers.Orders))]
        public virtual Shippers ShipViaNavigation { get; set; }
        public string CompanyName { get; set; }

      

        [StringLength(100)]
        public string ContactName { get; set; }
        [StringLength(50)]
        public string ContactTitle { get; set; }
    }
}