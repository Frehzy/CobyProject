using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
using HostData.Mapper;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class PaymentTypeController : BaseController, IPaymentTypeController
{
    private readonly IPaymentTypeService _paymentTypeService;

    public PaymentTypeController(IPaymentTypeService paymentTypeService, IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
        : base(waiterService, mapper, credentialsCache)
    {
        _paymentTypeService = paymentTypeService;
    }

    public async Task<PaymentTypeDto> CreatePaymentType(dynamic credentials, dynamic name, dynamic paymentTypeKind, dynamic needOpenCashBox)
    {
        Guid cId = CheckDynamicGuid(credentials);
        string n = Convert.ToString(name.ToString());
        PaymentTypeKind pTKindEnum = Enum.Parse<PaymentTypeKind>(paymentTypeKind);
        bool needOpen = Convert.ToBoolean(needOpenCashBox);
        var entityThatChanges = await CheckCredentials(cId);

        var paymentTypeModel = new PaymentTypeModel()
        {
            Name = n,
            Kind = pTKindEnum,
            NeedOpenCashBox = needOpen
        };
        await _paymentTypeService.Create(entityThatChanges.Id, paymentTypeModel);
        return PaymentFactory.CreateDto(paymentTypeModel);
    }

    public async Task<List<PaymentTypeDto>> GetPaymentTypes()
    {
        var paymentTypesModel = await _paymentTypeService.GetAll();
        return paymentTypesModel.Select(x => PaymentFactory.CreateDto(x)).ToList();
    }

    public async Task<PaymentTypeDto> GetPaymentTypeId(dynamic paymentTypeId)
    {
        Guid pTId = CheckDynamicGuid(paymentTypeId);
        var paymentTypeModel = await _paymentTypeService.GetById(pTId);
        return PaymentFactory.CreateDto(paymentTypeModel);
    }

    public async Task<PaymentTypeDto> RemovePaymentTypeById(dynamic credentials, dynamic paymentTypeId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid pTId = CheckDynamicGuid(paymentTypeId);

        var entityThatChanges = await CheckCredentials(cId);

        var paymentTypeModel = await _paymentTypeService.GetById(pTId);

        await _paymentTypeService.Remove(entityThatChanges.Id, pTId);
        return PaymentFactory.CreateDto(paymentTypeModel);
    }
}