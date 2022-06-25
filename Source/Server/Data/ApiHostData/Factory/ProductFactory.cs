using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Factory;

public static class ProductFactory
{
    public static ProductDto CreateDto(ProductModel productModel) =>
        new(productModel.Id,
            productModel.GuestId,
            productModel.WaiterId,
            productModel.PrintTime,
            productModel.Status,
            CreateDto(productModel.ProductItem),
            productModel.Comment);

    public static ProductItemDto CreateDto(ProductItemModel productItemModel) =>
        new(productItemModel.Id,
            productItemModel.Name,
            productItemModel.Price,
            productItemModel.Type);
}