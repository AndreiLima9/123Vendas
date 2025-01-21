using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using AndreiLima._123Vendas.Domain.Interfaces.Repositories;
using AndreiLima._123Vendas.Domain.Interfaces.Services;
using AndreiLima._123Vendas.Domain.Services.Notifications;


namespace AndreiLima._123Vendas.Domain.Services
{

    public class PurchaseService : ServiceBase<Purchase>, IPurchaseService
    {
        private readonly ICreatePurchasePublisher _createPurchasePublisher;
        private readonly IAlteredPurchasePublisher _alteredPurchasePublisher;
        private readonly ICanceledPurchasePublisher _canceledPurchasePublisher;
        private readonly IPurchaseRepository _repository;
        private readonly ISaleItemRepository _saleItemRepository;
        private ICreatePurchasePublisher createPurchasePublisherMock;
        private IAlteredPurchasePublisher alteredPurchasePublisherMock;

        public PurchaseService(
            ICreatePurchasePublisher createPurchasePublisher,
            IAlteredPurchasePublisher alteredPurchasePublisher,
            ICanceledPurchasePublisher canceledPurchasePublisher,
            IPurchaseRepository repository,
            ISaleItemRepository saleItemRepository           
            
           )
            : base(repository)
        {
            _createPurchasePublisher = createPurchasePublisher;
            _alteredPurchasePublisher = alteredPurchasePublisher;
            _canceledPurchasePublisher = canceledPurchasePublisher;
            _repository = repository;
            _saleItemRepository = saleItemRepository;          
            
            
        }


        public override async Task<Guid> CreateAsync(Purchase entity)
        {
            if (!entity.Check()) return Guid.Empty;

            entity.ApplyDiscount();
            var id = await base.CreateAsync(entity);

            _ = _createPurchasePublisher.PublishAsync(new CreatePurchaseMessage(entity));
            return id;
        }

        public async Task UpdateAsync(Guid id, Purchase purchase)
        {
            if (!purchase.Check()) return;

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                NotificationWrapper.Add("purchase", "Purchase não encontrada");
                return;
            }

            if (entity.Canceled)
            {
                NotificationWrapper.Add("purchase", "Esta purchase está cancelada e não pode ser alterada");
                return;
            }

            if (purchase.Items.Count > 0)
            {
                await _saleItemRepository.RemoveRangeAsync(entity.Items);

                entity.AlterItens(purchase.Items);

                await _saleItemRepository.AddRangeAsync(entity.Items);
                await _repository.UpdateAsync(entity);
            }

            _ = _alteredPurchasePublisher.PublishAsync(new AlteredPurchaseMessage(entity));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var purchase = await _repository.GetByIdAsync(id);

            if (purchase == null)
            {
                NotificationWrapper.Add("purchase", "Purchase não encontrada");
                return;
            }

            purchase.CancelPurchase();
            await _repository.UpdateAsync(purchase);

            _ = _canceledPurchasePublisher.PublishAsync(new CanceledPurchaseMessage(purchase));
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _repository.GetAsync(x => !x.Canceled);
        }
    }
}
