using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Clients
{
    // Simulação de chamada HTTP para obter o produto
 
        public class ProductClient : IProductClient
        {
            private readonly HttpClient _httpClient;

            public ProductClient(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<ProductDto> GetByIdAsync(string productId)
            {           
                return new ProductDto(productId, "Mocked Product Name");
            }
        }   
}
