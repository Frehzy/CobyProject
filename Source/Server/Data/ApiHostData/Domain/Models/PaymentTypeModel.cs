using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class PaymentTypeModel : BaseModel
{
    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public PaymentTypeModel() : base() { }
}