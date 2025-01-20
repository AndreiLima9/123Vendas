using AndreiLima._123Vendas.Domain.Services.Notifications;


namespace AndreiLima._123Vendas.Domain.Entities
{

    public class Purchase : EntityBase
    {
        protected Purchase() : base() { }

        public Purchase(string saleNumber, DateTime saleDate, string clientId, string storeCode) : base()
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            ClientId = clientId;
            StoreCode = storeCode;
            Canceled = false;
            Items = [];
        }

        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string ClientId { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal TotalDiscountValue { get; private set; }
        public string StoreCode { get; private set; }
        public bool Canceled { get; private set; }
        public DateTime? CanceledIn { get; private set; }

        public virtual ICollection<SaleItem> Items { get; private set; }

        public bool Check()
        {
            if (!Items.Any())
                NotificationWrapper.Add("purchase", "No items were added to the purchase");

            Items.ToList().ForEach(x => x.Check());

            return NotificationWrapper.IsValid;
        }

        public void ApplyDiscount()
        {
            Items.ToList().ForEach(x => x.ApplyDiscount());
            TotalDiscountValue = Items.Sum(x => x.ValueDiscount);
            CalcTotalValue();
        }

        private void CalcTotalValue()
        {
            TotalValue = Items.Sum(x => x.TotalValue);
        }

        public void CancelPurchase()
        {
            Canceled = true;
            CanceledIn = DateTime.Now;
        }

        public void AlterItens(IEnumerable<SaleItem> itens)
        {
            if (itens == null || !itens.Any())
            {
                NotificationWrapper.Add("items", "No items found to be changed");
                return;
            }

            Items = itens.ToList();
            ApplyDiscount();
        }
    }
}
