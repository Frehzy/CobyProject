using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class ProductModel : BaseModel
{
    public ProductItemModel ProductItem { get; set; }

    public Guid GuestId { get; set; }

    public GuestModel Guest { get; set; }

    public Guid WaiterId { get; set; }

    public WaiterModel Waiter { get; set; }

    public DateTime? PrintTime { get; set; }

    public string? Comment { get; set; }

    public ProductStatus Status { get; set; }

    public ProductModel() : base() { }
}