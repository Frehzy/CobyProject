using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class ProductFactory
{
    public static Product Create(IProduct product) =>
        new(product.Id,
            product.Name,
            product.Price,
            product.GuestId,
            product.WaiterId,
            product.PrintTime,
            product.Status,
            product.Type,
            product.IsDeleted,
            product.Comment);

    public static ProductDto CreateDto(IProduct product) =>
        new(product.Id,
            product.Name,
            product.Price,
            product.GuestId,
            product.WaiterId,
            product.PrintTime,
            product.Status,
            product.Type,
            product.IsDeleted,
            product.Comment);

    public static Product Create(ProductDto product) =>
        new(product.Id,
            product.Name,
            product.Price,
            product.GuestId,
            product.WaiterId,
            product.PrintTime,
            product.Status,
            product.Type,
            product.IsDeleted,
            product.Comment);
}
