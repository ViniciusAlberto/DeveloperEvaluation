using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    /// <summary>
    /// Command for retrieving a paginated list of sales
    /// </summary>
    public class ListSaleCommand : IRequest<ListSaleResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = "id asc";
    }
}
