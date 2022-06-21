using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Mapper;

public static class ProductMapper
{
    public static ProductDto CreateDto(ProductModel productModel) =>
        new(productModel.Id,
            productModel.GuestId,
            productModel.WaiterId,
            productModel.PrintTime,
            productModel.Status,
            CreateDto(productModel.ProductItem),
            productModel.IsDeleted,
            productModel.Comment);

    public static ProductItemDto CreateDto(ProductItemModel productItemModel) =>
        new(productItemModel.Id,
            productItemModel.Name,
            productItemModel.Price,
            productItemModel.Type,
            productItemModel.IsDeleted);
}