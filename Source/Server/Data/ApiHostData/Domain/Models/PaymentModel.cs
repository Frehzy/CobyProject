using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class PaymentModel : BaseModel
{
    public decimal Sum { get; set; }

    public PaymentTypeModel Type { get; set; }

    public PaymentStatus Status { get; set; }

    public PaymentModel() : base() { }
}