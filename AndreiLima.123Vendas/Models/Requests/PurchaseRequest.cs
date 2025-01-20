using System.ComponentModel.DataAnnotations;

namespace AndreiLima._123Vendas.Models.Requests
{
    public class PurchaseRequest
    {
        [Required, MinLength(2)]
        public string SaleNumber { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        [Required, MinLength(5)]
        public string ClientId { get; set; }
        [Required]
        public string StoreCode { get; set; }
        [Required]
        public List<SaleItemRequest> Items { get; set; }
    }
}
