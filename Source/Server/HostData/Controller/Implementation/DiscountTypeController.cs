using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
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
        var cId = (Guid)CheckDynamicGuid(credentials);
        var n = (string)Convert.ToString(name);
        var entityThatChanges = await CheckCredentials(cId);

        var discountTypeModel = new DiscountTypeModel()
        {
            Name = n
        };
        await _discountTypeService.Create(entityThatChanges.Id, discountTypeModel);
        return Mapper.Map<DiscountTypeModel, DiscountTypeDto>(discountTypeModel);
    }

    public async Task<DiscountTypeDto> GetDiscountTypeId(dynamic discountTypeId)
    {
        var dTIp = (Guid)CheckDynamicGuid(discountTypeId);
        var discountTypeModel = await _discountTypeService.GetById(dTIp);
        return Mapper.Map<DiscountTypeModel, DiscountTypeDto>(discountTypeModel);
    }

    public async Task<List<DiscountTypeDto>> GetDiscountTypes()
    {
        var discountTypeModel = await _discountTypeService.GetAll();
        return discountTypeModel.Select(x => Mapper.Map<DiscountTypeModel, DiscountTypeDto>(x)).ToList();
    }

    public async Task<DiscountTypeDto> RemoveDiscountTypeById(dynamic credentials, dynamic discountTypeId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var dTIp = (Guid)CheckDynamicGuid(discountTypeId);

        var entityThatChanges = await CheckCredentials(cId);

        var discountTypeModel = await _discountTypeService.GetById(dTIp);

        await _discountTypeService.Remove(entityThatChanges.Id, dTIp);
        return Mapper.Map<DiscountTypeModel, DiscountTypeDto>(discountTypeModel);
    }
}