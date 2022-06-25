using ApiHostData.Cache.Credentials;
using ApiHostData.Controller.Contract;
using ApiHostData.Domain.Models;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Shared.Data.Enum;
using Shared.Factory.Dto;
using SharedData.Mapper;

namespace ApiHostData.Controller.Implementation;

public class ProductItemController : BaseController, IProductItemController
{
    private readonly IProductItemService _productItemService;

    public ProductItemController(IProductItemService productItemService, IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
        _productItemService = productItemService;
    }

    public async Task<ProductItemDto> CreateProductItem(dynamic credentials, dynamic name, dynamic price, dynamic productType)
    {
        Guid cId = CheckDynamicGuid(credentials);
        string n = Convert.ToString(name.ToString());
        decimal p = decimal.Parse(price);
        ProductType pTypeEnum = Enum.Parse<ProductType>(productType);
        var entityThatChanges = await CheckCredentials(cId);

        var productItemModel = new ProductItemModel()
        {
            Name = n,
            Price = p,
            Type = pTypeEnum,
        };
        await _productItemService.Create(entityThatChanges.Id, productItemModel);
        return ProductFactory.CreateDto(productItemModel);
    }

    public async Task<ProductItemDto> GetProductItemId(dynamic productItemId)
    {
        Guid pIId = CheckDynamicGuid(productItemId);
        var productItemModel = await _productItemService.GetById(pIId);
        return ProductFactory.CreateDto(productItemModel);
    }

    public async Task<List<ProductItemDto>> GetProductItems()
    {
        var productItemsModel = await _productItemService.Get();
        return productItemsModel.Select(x => ProductFactory.CreateDto(x)).ToList();
    }

    public async Task<ProductItemDto> RemoveProductItemById(dynamic credentials, dynamic productItemId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid pIId = CheckDynamicGuid(productItemId);

        var entityThatChanges = await CheckCredentials(cId);

        var productItemModel = await _productItemService.GetById(pIId);

        await _productItemService.Remove(entityThatChanges.Id, pIId);
        return ProductFactory.CreateDto(productItemModel);
    }
}