namespace AndreiLima._123Vendas.Models.Responses
{
    public class PurchaseResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientId { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalDiscontValue { get; set; }
        public string StoreCode { get; set; }
        public bool Canceled { get; set; }
        public DateTime? CancelledIn { get; set; }
    }
}
