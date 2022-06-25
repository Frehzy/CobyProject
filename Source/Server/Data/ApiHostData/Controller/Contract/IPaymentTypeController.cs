using Shared.Factory.Dto;

namespace ApiHostData.Controller.Contract;

public interface IPaymentTypeController
{
    public Task<PaymentTypeDto> CreatePaymentType(dynamic credentials, dynamic name, dynamic paymentTypeKind, dynamic needOpenCashBox);

    public Task<PaymentTypeDto> RemovePaymentTypeById(dynamic credentials, dynamic paymentTypeId);

    public Task<PaymentTypeDto> GetPaymentTypeId(dynamic paymentTypeId);

    public Task<List<PaymentTypeDto>> GetPaymentTypes();
}