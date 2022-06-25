using Shared.Factory.Dto;

namespace ApiHostData.Controller.Contract;

public interface IProductItemController
{
    public Task<ProductItemDto> CreateProductItem(dynamic credentials, dynamic name, dynamic price, dynamic productType);

    public Task<ProductItemDto> RemoveProductItemById(dynamic credentials, dynamic productItemId);

    public Task<ProductItemDto> GetProductItemId(dynamic productItemId);

    public Task<List<ProductItemDto>> GetProductItems();
}