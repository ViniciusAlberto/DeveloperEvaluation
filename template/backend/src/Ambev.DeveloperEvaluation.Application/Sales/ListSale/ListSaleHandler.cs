using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    /// <summary>
    /// Handler for processing ListSaleCommand requests
    /// </summary>
    public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public ListSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<ListSaleResult> Handle(ListSaleCommand request, CancellationToken cancellationToken)
        {
           
            var validator = new ListSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var (sales, totalCount) = await _saleRepository.GetPagedAsync(
                request.Page,
                request.PageSize,
                request.OrderBy,
                cancellationToken
            );

            var items = _mapper.Map<IEnumerable<SaleDto>>(sales);

            return new ListSaleResult
            {
                Items = items,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                TotalCount = totalCount
            };
        }
    }
}
