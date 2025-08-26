using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, SaleItemDto>();

            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleResult, SaleBaseResponse>();
            CreateMap<CreateSaleItemResult, SaleItemBaseResponse>();
        }
    }
}
