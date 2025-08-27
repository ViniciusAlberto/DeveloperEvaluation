using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    internal class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {

            CreateMap<Sale, SaleDto>()
                    .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                    .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => src.Cancelled))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, SaleItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity))
                .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => src.IsCancelled));
        }
    }
}
