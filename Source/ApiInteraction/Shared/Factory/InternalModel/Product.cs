using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Product : IProduct
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid GuestId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime? PrintTime { get; set; }

    public ProductStatus Status { get; set; }

    public ProductType Type { get; set; }

    public bool IsDeleted { get; set; }

    public string? Comment { get; set; }

    public Product() { }

    public Product(Guid id, string name, decimal price, Guid guestId, Guid waiterId, DateTime? printTime, ProductStatus status, ProductType type,  bool isDeleted, string? comment)
    {
        Id = id;
        Name = name;
        Price = price;
        GuestId = guestId;
        WaiterId = waiterId;
        PrintTime = printTime;
        Status = status;
        Type = type;
        IsDeleted = isDeleted;
        Comment = comment;
    }
}