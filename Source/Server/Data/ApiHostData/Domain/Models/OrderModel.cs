using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class OrderModel : BaseModel
{
    public int Number { get; set; }

    public WaiterModel Waiter { get; set; }

    public List<TableModel> Tables { get; set; } = new();

    public List<GuestModel> Guests { get; set; } = new();

    public List<ProductModel> Products { get; set; } = new();

    public List<DiscountModel> Discounts { get; set; } = new();

    public List<PaymentModel> Payments { get; set; } = new();

    public int Version { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime StartTime { get; set; } = DateTime.Now;

    public DateTime? CloseTime { get; set; }

    public OrderModel() : base() { }
}