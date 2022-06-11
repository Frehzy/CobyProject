using Shared.Data.Enum;

namespace Shared.Data;

public interface IProduct
{
    public Guid Id { get; }

    public string Name { get; }

    public decimal Price { get; } 

    public Guid GuestId { get; }

    public Guid WaiterId { get; }

    public DateTime? PrintTime { get; }

    public ProductStatus Status { get; }

    public bool IsDeleted { get; }

    public string? Comment { get; }
}