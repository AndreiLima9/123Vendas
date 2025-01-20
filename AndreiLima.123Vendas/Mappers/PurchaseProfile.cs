using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Models.Requests;
using AndreiLima._123Vendas.Models.Responses;
using AutoMapper;

namespace AndreiLima._123Vendas.Mappers
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<SaleItemRequest, SaleItem>();
            CreateMap<PurchaseRequest, Purchase>();

            CreateMap<SaleItem, SaleItemResponse>();
            CreateMap<Purchase, PurchaseResponse>();
            CreateMap<Purchase, PurchaseDetailsResponse>();
        }
    }
}
