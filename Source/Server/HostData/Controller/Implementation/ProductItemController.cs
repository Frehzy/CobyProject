using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
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
        var cId = (Guid)CheckDynamicGuid(credentials);
        var n = (string)Convert.ToString(name);
        var p = (decimal)decimal.Parse(price);
        var pTypeEnum = (ProductType)Enum.Parse<ProductType>(productType);
        var entityThatChanges = await CheckCredentials(cId);

        var productItemModel = new ProductItemModel()
        {
            Name = n,
            Price = p,
            Type = pTypeEnum,
        };
        await _productItemService.Create(entityThatChanges.Id, productItemModel);
        return Mapper.Map<ProductItemModel, ProductItemDto>(productItemModel);
    }

    public async Task<ProductItemDto> GetProductItemId(dynamic productItemId)
    {
        var pIId = (Guid)CheckDynamicGuid(productItemId);
        var productItemModel = await _productItemService.GetById(pIId);
        return Mapper.Map<ProductItemModel, ProductItemDto>(productItemModel);
    }

    public async Task<List<ProductItemDto>> GetProductItems()
    {
        var productItemsModel = await _productItemService.GetAll();
        return productItemsModel.Select(x => Mapper.Map<ProductItemModel, ProductItemDto>(x)).ToList();
    }

    public async Task<ProductItemDto> RemoveProductItemById(dynamic credentials, dynamic productItemId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var pIId = (Guid)CheckDynamicGuid(productItemId);

        var entityThatChanges = await CheckCredentials(cId);

        var productItemModel = await _productItemService.GetById(pIId);

        await _productItemService.Remove(entityThatChanges.Id, pIId);
        return Mapper.Map<ProductItemModel, ProductItemDto>(productItemModel);
    }
}