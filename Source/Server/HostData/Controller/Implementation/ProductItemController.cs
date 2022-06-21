using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
using HostData.Mapper;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

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
        var productItemsModel = await _productItemService.GetAll();
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