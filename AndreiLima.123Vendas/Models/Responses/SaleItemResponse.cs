namespace AndreiLima._123Vendas.Models.Responses
{
    public class SaleItemResponse
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}
