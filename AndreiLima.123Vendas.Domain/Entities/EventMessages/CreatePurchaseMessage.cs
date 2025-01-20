﻿namespace AndreiLima._123Vendas.Domain.Entities.EventMessages
{
    public class CreatePurchaseMessage : EventMessageBase
    {
        public CreatePurchaseMessage(Purchase purchase)
        {
            PurchaseId = purchase.Id;
            ClientId = purchase.ClientId;
            TotalValue = purchase.TotalValue;
        }

        public Guid PurchaseId { get; set; }
        public string ClientId { get; set; }
        public decimal TotalValue { get; set; }
    }
}
