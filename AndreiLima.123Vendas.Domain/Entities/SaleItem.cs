using AndreiLima._123Vendas.Domain.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreiLima._123Vendas.Domain.Entities
{
    public class SaleItem : EntityBase
    {
        public SaleItem(string productCode, int quantity, decimal unitValue)
        {
            Id = Guid.NewGuid();
            ProductCode = productCode;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public string ProductCode { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public decimal Discount { get; private set; }
        public decimal ValueDiscount { get; private set; }
        public decimal TotalValue { get; private set; }

        public virtual Purchase Purchase { get; protected set; }

        public bool Check()
        {
            if (Quantity > 20)
                NotificationWrapper.Add($"item.{ProductCode}", "It is not possible to sell more than 20 identical items.");

            if (Quantity < 4 && Discount > 0)
                NotificationWrapper.Add($"item.{ProductCode}", "Purchases of less than 4 items cannot be discounted.");

            return NotificationWrapper.IsValid;
        }

        public void Update(int quantity, decimal unitValue)
        {
            Quantity = quantity;
            UnitValue = unitValue;
            ApplyDiscount();
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
                Discount = 0.2m;
            else if (Quantity >= 4)
                Discount = 0.1m;
            else
                Discount = 0m;

            ValueDiscount = UnitValue * Quantity * Discount;
            CalcTotalValue();
        }

        private void CalcTotalValue()
        {
            TotalValue = (Quantity * UnitValue) - ValueDiscount;
        }
    }
}
