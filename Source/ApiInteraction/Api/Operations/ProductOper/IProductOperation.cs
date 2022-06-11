using Shared.Data;

namespace Api.Operations.ProductOper;

public interface IProductOperation
{
    public IReadOnlyList<IProduct> AddProduct(IOrder order, ICredentials credentials, IProduct product, ref ISession session);

    public IReadOnlyList<IProduct> RemoveProduct(IOrder order, ICredentials credentials, IProduct product, ref ISession session);

    public IReadOnlyList<IProduct> GetProducts();
}