using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItemSale
{
    public class CancelSaleItemsProfile : Profile
    {
        public CancelSaleItemsProfile()
        {
            CreateMap<CancelSaleItemsRequest, CancelSaleItemsCommand>();
        }
    }
}
