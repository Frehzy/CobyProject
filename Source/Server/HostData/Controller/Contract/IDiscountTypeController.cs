using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface IDiscountTypeController
{
    public Task<DiscountTypeDto> CreateDiscountType(dynamic credentials, dynamic name);

    public Task<DiscountTypeDto> RemoveDiscountTypeById(dynamic credentials, dynamic discountTypeId);

    public Task<DiscountTypeDto> GetDiscountTypeId(dynamic discountTypeId);

    public Task<List<DiscountTypeDto>> GetDiscountTypes();
}