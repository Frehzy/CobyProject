using Shared.Data.Enum;

namespace HostData.EntityData.Order;

public class EntityOrderProduct
{
    public int OperationNumber { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public Guid GuestId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime? PrintTime { get; set; }

    public ProductStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public string? Comment { get; set; }
}