using ApiHostData.Controller.Contract;
using Shared.Factory.Dto;
using SharedData.Modules;

namespace ApiHostData.Modules;

public class ProductItemModule : BaseModule
{
    private readonly IProductItemController _productItemController;

    public ProductItemModule(IProductItemController productItemController) : base()
    {
        _productItemController = productItemController;

        Get("/products", async parameters =>
        {
            return await Execute(Context, () => _productItemController.GetProductItems());
        });

        Get("/product/{productId}", async parameters =>
        {
            var productId = parameters.productId;
            return await Execute<ProductItemDto>(Context, () => _productItemController.GetProductItemId(productId));
        });

        Get("{credentialsId}/product/create/{name}/{price}/{productType}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var name = parameters.name;
            var price = parameters.price;
            var productType = parameters.productType;
            return await Execute<ProductItemDto>(Context, () => _productItemController.CreateProductItem(credentialsId, name, price, productType));
        });

        Get("{credentialsId}/product/remove/{productId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var productId = parameters.productId;
            return await Execute<ProductItemDto>(Context, () => _productItemController.RemoveProductItemById(credentialsId, productId));
        });
    }
}