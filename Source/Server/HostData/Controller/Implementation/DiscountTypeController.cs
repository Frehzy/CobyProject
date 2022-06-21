using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
using HostData.Mapper;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class DiscountTypeController : BaseController, IDiscountTypeController
{
    private readonly IDiscountTypeService _discountTypeService;

    public DiscountTypeController(IDiscountTypeService discountTypeService, IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
        _discountTypeService = discountTypeService;
    }

    public async Task<DiscountTypeDto> CreateDiscountType(dynamic credentials, dynamic name)
    {
        Guid cId = CheckDynamicGuid(credentials);
        string n = Convert.ToString(name.ToString());
        var entityThatChanges = await CheckCredentials(cId);

        var discountTypeModel = new DiscountTypeModel()
        {
            Name = n
        };
        await _discountTypeService.Create(entityThatChanges.Id, discountTypeModel);
        return DiscountFactory.CreateDto(discountTypeModel);
    }

    public async Task<DiscountTypeDto> GetDiscountTypeId(dynamic discountTypeId)
    {
        Guid dTIp = CheckDynamicGuid(discountTypeId);
        var discountTypeModel = await _discountTypeService.GetById(dTIp);
        return DiscountFactory.CreateDto(discountTypeModel);
    }

    public async Task<List<DiscountTypeDto>> GetDiscountTypes()
    {
        var discountTypeModel = await _discountTypeService.Get();
        return discountTypeModel.Select(x => DiscountFactory.CreateDto(x)).ToList();
    }

    public async Task<DiscountTypeDto> RemoveDiscountTypeById(dynamic credentials, dynamic discountTypeId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid dTIp = CheckDynamicGuid(discountTypeId);

        var entityThatChanges = await CheckCredentials(cId);

        var discountTypeModel = await _discountTypeService.GetById(dTIp);

        await _discountTypeService.Remove(entityThatChanges.Id, dTIp);
        return DiscountFactory.CreateDto(discountTypeModel);
    }
}