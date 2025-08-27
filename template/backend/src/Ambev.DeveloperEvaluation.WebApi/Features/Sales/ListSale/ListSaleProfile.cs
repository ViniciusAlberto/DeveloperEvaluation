using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<Application.Sales.ListSale.SaleDto, SaleDto>();

            CreateMap<Application.Sales.ListSale.SaleItemDto,  SaleItemDto>();
        }
    }
}