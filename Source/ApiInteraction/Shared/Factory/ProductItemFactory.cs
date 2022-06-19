using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class ProductItemFactory
{
    public static ProductItem Create(IProductItem productItem) =>
        new(productItem.Id, productItem.Name, productItem.Price, productItem.Type, productItem.IsDeleted);

    public static ProductItemDto CreateDto(IProductItem productItem) =>
        new(productItem.Id, productItem.Name, productItem.Price, productItem.Type, productItem.IsDeleted);

    public static ProductItem Create(ProductItemDto productItem) =>
        new(productItem.Id, productItem.Name, productItem.Price, productItem.Type, productItem.IsDeleted);
}