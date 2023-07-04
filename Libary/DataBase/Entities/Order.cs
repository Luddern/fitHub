
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Libary.DataBase.Entities
{
 [Table("Orders")]
    public partial class Order
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; }

        [Column("Customer_ID")]
        public string CustomerId { get; set; }

        [Column("TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Column("Status")]
        public int Status { get; set; }

        [Column("Order_Date")]
        public DateTime OrderDate { get; set; }

        [Column("Sales_Name")]
        public string SalesName { get; set; }
    }
}