using System.ComponentModel.DataAnnotations;

namespace AndreiLima._123Vendas.Models.Requests
{
    public class SaleItemRequest
    {
        [Required, MinLength(5)]
        public string ProductCode { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitValue { get; set; }
    }
}
