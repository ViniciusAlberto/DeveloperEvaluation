using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleRequest, GetSaleCommand>();            

            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleResult, SaleBaseResponse>();
            CreateMap<GetSaleItemResult, SaleItemBaseResponse>();
        }
    }
}
