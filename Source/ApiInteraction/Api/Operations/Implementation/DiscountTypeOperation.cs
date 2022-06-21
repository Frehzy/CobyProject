using Api.Http;
using Api.Operations.Contracts;
using Shared.Data;
using Shared.Factory;
using Shared.Factory.Dto;

namespace Api.Operations.Implementation;

internal class DiscountTypeOperation : IDiscountTypeOperation
{
    public IDiscountType CreateDiscountType(ICredentials credentials, string name)
    {
        var result = HttpRequest.Request<DiscountTypeDto>($"{credentials.Id}/discountType/create/{name}");
        return DiscountTypeFactory.Create(result);
    }

    public IDiscountType GetDiscountTypeById(Guid discountTypeId)
    {
        var result = HttpRequest.Request<DiscountTypeDto>($"discountType/{discountTypeId}");
        return DiscountTypeFactory.Create(result);
    }

    public IReadOnlyList<IDiscountType> GetDiscountTypes()
    {
        var result = HttpRequest.Request<List<DiscountTypeDto>>($"discountTypes");
        return result.Select(x => DiscountTypeFactory.Create(x)).ToList();
    }
    public bool RemoveDiscountType(ICredentials credentials, IDiscountType discountType)
    {
        return HttpRequest.Request<DiscountTypeDto>($"{credentials.Id}/discountType/remove/{discountType.Id}") is not null;
    }
}