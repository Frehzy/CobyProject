using Shared.Data.Enum;

namespace Shared.Data;

public interface IProduct
{
    public Guid Id { get; }

    public Guid GuestId { get; }

    public Guid WaiterId { get; }

    public DateTime? PrintTime { get; }

    public ProductStatus Status { get; }

    public IProductItem ProductItem { get; }

    public string? Comment { get; }
}