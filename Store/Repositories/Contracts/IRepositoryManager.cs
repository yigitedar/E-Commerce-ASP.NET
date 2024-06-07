namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product {get; }
        ICategoryRepository Category {get;}
        IOrderRepository Order {get;}
        IInvoiceRepository Invoice { get; }
        void Save();
    }
}