using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Sale command/DTOs and Domain entities
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>();

            CreateMap<CustomerDto, ExternalCustomer>()
                .ConstructUsing(dto => new ExternalCustomer(dto.ExternalId, dto.Name));

            CreateMap<BranchDto, ExternalBranch>()
                .ConstructUsing(dto => new ExternalBranch(dto.ExternalId, dto.Name));


            // ProductDto -> ExternalProduct (para mapear retorno do ProductClient)
            CreateMap<ProductDto, ExternalProduct>()
                .ConstructUsing(dto => new ExternalProduct(dto.ExternalId, dto.Name));

            // Sale -> CreateSaleResult (resposta do handler)
            CreateMap<Sale, CreateSaleResult>()
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
    