namespace AndreiLima._123Vendas.Models.Responses
{
    public class PurchaseDetailsResponse : PurchaseResponse
    {
        public List<SaleItemResponse> Items { get; set; }
    }
}
