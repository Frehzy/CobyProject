using Shared.Data;

namespace Api.Operations.ProductOper;

internal class ProductOperation : IProductOperation
{
    public IReadOnlyList<IProduct> AddProduct(IOrder order, IProduct product, ref ISession session)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<IProduct> RemoveProduct(IOrder order, IProduct guest, ref ISession session)
    {
        throw new NotImplementedException();
    }
}