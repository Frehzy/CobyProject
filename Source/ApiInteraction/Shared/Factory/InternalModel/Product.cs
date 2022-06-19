using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Product : IProduct
{
    public Guid Id { get; set; }

    public Guid GuestId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime? PrintTime { get; set; }

    public ProductStatus Status { get; set; }

    public IProductItem ProductItem { get; set; }

    public bool IsDeleted { get; set; }

    public string? Comment { get; set; }

    public Product() { }

    public Product(Guid id, Guid guestId, Guid waiterId, DateTime? printTime, ProductStatus status, IProductItem productItem, bool isDeleted, string? comment)
    {
        Id = id;
        GuestId = guestId;
        WaiterId = waiterId;
        PrintTime = printTime;
        Status = status;
        ProductItem = productItem;
        IsDeleted = isDeleted;
        Comment = comment;
    }
}