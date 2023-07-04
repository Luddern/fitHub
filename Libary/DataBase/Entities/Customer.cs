


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Libary.DataBase.Entities
{
    [Table("Customers")]
    public partial class Customer
    {
        [Key]
        [Column("UID")]
        public string Uid { get; set; }

        [Column("ID")]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Zip")]
        public string Zip { get; set; }

        [Column("Status")]
        public int? Status { get; set; }
    }
}