using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

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

    public DateTime StartTime { get; set; }

    public DateTime? CloseTime { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public OrderModel() { }
}