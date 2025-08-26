namespace Ambev.DeveloperEvaluation.Domain.Clients
{

    public interface IProductClient
    {
        Task<ProductDto> GetByIdAsync(string productId);
    }

}
