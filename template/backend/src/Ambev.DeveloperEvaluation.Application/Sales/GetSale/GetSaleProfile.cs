using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping between Sale command/DTOs and Domain entities
    /// </summary>
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {      
            CreateMap<Sale, GetSaleResult>()
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.ExternalId))
             .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.ExternalId))
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(i => new CreateSaleItemResult
             {
                 ProductId = i.Product.ExternalId,
                 ProductName = i.Product.Name,
                 Quantity = i.Quantity,
                 UnitPrice = i.UnitPrice,
                 Subtotal = i.Total
             })))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
