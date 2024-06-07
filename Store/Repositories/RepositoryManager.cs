using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public RepositoryManager(IProductRepository productRepository,
        RepositoryContext context,
        ICategoryRepository categoryRepository,
        IOrderRepository orderRepository,
        IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order => _orderRepository;

        public IInvoiceRepository Invoice => _invoiceRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}