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
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/discountType/create/{name}");
        var result = Task.Run(async () => await HttpRequest.Get<DiscountTypeDto>(uri)).Result;
        return DiscountTypeFactory.Create(result.Content);
    }

    public IDiscountType GetDiscountTypeById(Guid discountTypeId)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"discountType/{discountTypeId}");
        var result = Task.Run(async () => await HttpRequest.Get<DiscountTypeDto>(uri)).Result;
        return DiscountTypeFactory.Create(result.Content);
    }

    public IReadOnlyList<IDiscountType> GetDiscountTypes()
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, "discountTypes");
        var result = Task.Run(async () => await HttpRequest.Get<List<DiscountTypeDto>>(uri)).Result;
        return result.Content.Select(x => DiscountTypeFactory.Create(x)).ToList();
    }
    public bool RemoveDiscountType(ICredentials credentials, IDiscountType discountType)
    {
        var ip = ModuleOperation.NetOperation.GetLocalIPAddress();
        var uri = HttpUtility.CreateUri(ip.ToString(), 5050, $"{credentials.Id}/discountType/remove/{discountType.Id}");
        var result = Task.Run(async () => await HttpRequest.Get<DiscountTypeDto>(uri)).Result;
        return result.Content is not null;
    }
}