
namespace Ambev.DeveloperEvaluation.Domain.Clients
{
    public class ProductDto
    {
        //não necessário, apenas para simular o retorno externo
        public ProductDto(string productId, string v)
        {
        }

        public string ExternalId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
       
    }
}
