using Shared.Data;

namespace Api.Operations.ProductOper;

public interface IProductOperation
{
    public IReadOnlyList<IProduct> AddProduct(ICredentials credentials, IProduct product, ref ISession session);

    public IReadOnlyList<IProduct> RemoveProduct(ICredentials credentials, IProduct product, ref ISession session);

    public IReadOnlyList<IProduct> GetProducts();
}