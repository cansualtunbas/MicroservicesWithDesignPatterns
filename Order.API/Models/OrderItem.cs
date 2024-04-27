using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        //toplam 18 karakter. virgülden sonra 2 karakter
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
