using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Modules;

/*public class TestModule : BaseModule
{
    private readonly IDbRepository _dbRepository;
    private readonly IOrderService _orderService;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public TestModule(IDbRepository dbRepository, IOrderService orderService, IDiscountService discountService) : base()
    {
        _dbRepository = dbRepository;
        _orderService = orderService;
        _discountService = discountService;

        var discountList = _discountService.GetAll().Result;
    }
}*/