using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using AndreiLima._123Vendas.Domain.Interfaces.Repositories;
using AndreiLima._123Vendas.Domain.Interfaces.Services;
using AndreiLima._123Vendas.Domain.Services;
using Bogus;
using NSubstitute;

namespace AndreiLima._123Vendas.Infrastructure.UnitTests.PurchaseTest
{
    public class PurchaseServiceTest
    {
        private readonly IPurchaseRepository _repositoryMock;
        private readonly ISaleItemRepository _saleItemRepositoryMock;
        private readonly ICreatePurchasePublisher _createPurchasePublisherMock;
        private readonly IAlteredPurchasePublisher _alteredPurchasePublisherMock;
        private readonly ICanceledPurchasePublisher _canceledPurchasePublisherMock;
        private readonly IPurchaseService _purchaseService;
        private readonly Faker _faker;

        public PurchaseServiceTest()
        {
            DI.DIConfig.AddDependencies();

            _repositoryMock = Substitute.For<IPurchaseRepository>();
            _saleItemRepositoryMock = Substitute.For<ISaleItemRepository>();
            _createPurchasePublisherMock = Substitute.For<ICreatePurchasePublisher>();
            _alteredPurchasePublisherMock = Substitute.For<IAlteredPurchasePublisher>();
            _canceledPurchasePublisherMock = Substitute.For<ICanceledPurchasePublisher>();
            _purchaseService = new PurchaseService(
                _createPurchasePublisherMock,
                _alteredPurchasePublisherMock,
                _canceledPurchasePublisherMock,
                _repositoryMock,
                _saleItemRepositoryMock
            );
            _faker = new Faker();
        }

        [Fact]
        public async Task CreatePurchaseAsync_ValidPurchase_ShouldSavePurchaseAndPublishEvent()
        {
            // Arrange: Create a valid purchase with multiple items.
            var purchase = CreateValidPurchase();
            purchase.AlterItens(new[]
            {
                CreateSaleItem(2, 5.22m),
                CreateSaleItem(3, 6.22m),
                CreateSaleItem(4, 7.22m)
            });

            // Act: Attempt to create the purchase.
            await _purchaseService.CreateAsync(purchase);

            // Assert: Ensure the purchase was saved and an event was published.
            await _repositoryMock.Received(1).CreateAsync(purchase);
            await _createPurchasePublisherMock.Received(1).PublishAsync(Arg.Is<CreatePurchaseMessage>(message => message.PurchaseId == purchase.Id));
        }

        [Fact]
        public async Task CreatePurchaseAsync_InvalidPurchase_ShouldNotSaveOrPublishEvent()
        {
            // Arrange: Create an invalid purchase (e.g., missing required details).
            var purchase = new Purchase(null, DateTime.Now, GenerateRandomString(12), GenerateRandomString(6)); // ID nulo ou inválido
            purchase.AlterItens(new[]
            {
                new SaleItem(GenerateRandomString(5), -1, 5.22m) // Quantidade negativa ou outro erro lógico
            });

            // Act: Attempt to create the invalid purchase.
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _purchaseService.CreateAsync(purchase));

            // Assert: Ensure no calls to CreateAsync or PublishAsync were made.
            await _repositoryMock.DidNotReceive().CreateAsync(Arg.Any<Purchase>());
            await _createPurchasePublisherMock.DidNotReceive().PublishAsync(Arg.Any<CreatePurchaseMessage>());
        }


        [Fact]
        public async Task UpdatePurchaseAsync_ValidAlteration_ShouldUpdatePurchaseAndPublishEvent()
        {
            // Arrange: Setup an existing purchase and a modified version.
            var existingPurchase = CreateValidPurchase();
            _repositoryMock.GetByIdAsync(existingPurchase.Id).Returns(existingPurchase);

            var updatedPurchase = CreateValidPurchase();
            updatedPurchase.AlterItens(new[]
            {
                CreateSaleItem(4, 7.22m),
                CreateSaleItem(5, 8.22m)
            });

            // Act: Attempt to update the purchase.
            await _purchaseService.UpdateAsync(existingPurchase.Id, updatedPurchase);

            // Assert: Ensure the purchase was updated and an event was published.
            await _repositoryMock.Received(1).UpdateAsync(existingPurchase);
            await _saleItemRepositoryMock.Received(1).RemoveRangeAsync(Arg.Any<IEnumerable<SaleItem>>());
            await _saleItemRepositoryMock.Received(1).AddRangeAsync(Arg.Any<IEnumerable<SaleItem>>());
            await _alteredPurchasePublisherMock.Received(1).PublishAsync(Arg.Is<AlteredPurchaseMessage>(message => message.PurchaseId == existingPurchase.Id));
        }

        [Fact]
        public async Task DeletePurchaseAsync_ValidId_ShouldCancelPurchaseAndPublishEvent()
        {
            // Arrange: Setup an existing purchase.
            var purchase = CreateValidPurchase();
            _repositoryMock.GetByIdAsync(purchase.Id).Returns(purchase);

            // Act: Attempt to cancel the purchase.
            await _purchaseService.DeleteAsync(purchase.Id);

            // Assert: Ensure the purchase was marked as canceled and an event was published.
            await _repositoryMock.Received(1).UpdateAsync(purchase);
            await _canceledPurchasePublisherMock.Received(1).PublishAsync(Arg.Is<CanceledPurchaseMessage>(message => message.PurchaseId == purchase.Id));
        }

        // Utility methods for creating test data
        private Purchase CreateValidPurchase()
        {
            return new Purchase(
                GenerateRandomString(10),
                DateTime.Now,
                GenerateRandomString(12),
                GenerateRandomString(4)
            );
        }

        private SaleItem CreateSaleItem(int quantity, decimal price)
        {
            return new SaleItem(GenerateRandomString(5), quantity, price);
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
