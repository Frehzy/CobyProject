using HostData.Controller.Contract;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class DiscountTypeService : BaseModule
{
    private readonly IDiscountTypeController _discountTypeController;

    public DiscountTypeService(IDiscountTypeController discountTypeController) : base()
    {
        _discountTypeController = discountTypeController;

        Get("/discountTypes", async parameters =>
        {
            return await Execute(Context, () => _discountTypeController.GetDiscountTypes());
        });

        Get("/discountType/{discountTypeId}", async parameters =>
        {
            var discountTypeId = parameters.discountTypeId;
            return await Execute<DiscountTypeDto>(Context, () => _discountTypeController.GetDiscountTypeId(discountTypeId));
        });

        Get("{credentialsId}/discountType/create/{discountTypeName}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var discountTypeName = parameters.discountTypeName;
            return await Execute<DiscountTypeDto>(Context, () => _discountTypeController.CreateDiscountType(credentialsId, discountTypeName));
        });

        Get("{credentialsId}/discountType/remove/{discountTypeId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var discountTypeId = parameters.discountTypeId;
            return await Execute<DiscountTypeDto>(Context, () => _discountTypeController.RemoveDiscountTypeById(credentialsId, discountTypeId));
        });
    }
}