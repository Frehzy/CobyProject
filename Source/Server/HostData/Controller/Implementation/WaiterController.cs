using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Mapper.Factory;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class WaiterController : BaseController, IWaiterController
{
    private readonly IWaiterService _waiterService;

    public WaiterController(IMapper mapper, IWaiterService waiterService, IWaiterPermissionService waiterPermissionService)
        : base(mapper, waiterPermissionService)
    {
        _waiterService = waiterService;
    }

    public async Task<WaiterDto> CreateWaiter(string name, string password)
    {
        var waiterModel = new WaiterModel()
        {
            Name = name,
            Password = password,
            IsSessionOpen = false
        };
        await _waiterService.Create(waiterModel);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        var waitersPermissionModel = await WaiterPermissionService.GetAll();
        return waitersPermissionModel.Select(x => WaiterFactory.CreateDto(x)).ToList();
    }
}