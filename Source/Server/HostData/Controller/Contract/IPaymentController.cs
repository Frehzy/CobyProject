using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface IPaymentController
{
    public Task<PaymentTypeDto> CreatePaymentType(dynamic credentials, dynamic name, dynamic paymentTypeKind, dynamic needOpenCashBox);

    public Task<PaymentTypeDto> RemovePaymentTypeById(dynamic credentials, dynamic paymentTypeId);

    public Task<PaymentTypeDto> GetPaymentTypeyId(dynamic paymentTypeId);

    public Task<List<PaymentTypeDto>> GetPaymentTypes();
}