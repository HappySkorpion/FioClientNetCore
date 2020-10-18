using System;
using System.Threading.Tasks;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class ApiClientTests
        : IDisposable,
        IClassFixture<TokenFixture>
    {
        private readonly ApiClient _client;

        #region Initialization and Clenaup

        public ApiClientTests(
            TokenFixture tokenFixture)
        {
            _client = new ApiClient(tokenFixture.Token);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        [Fact]
        public async Task ListTransactionsInPeriod_Pass()
        {
            var result = await _client
                .ListTransactionsAsync(DateTime.Today.AddDays(-1), DateTime.Today);
        }

        [Fact]
        public async Task ListTransactionsIn_Pass()
        {
            var result = await _client
                .ListTransactionsAsync(2020, 1);
        }

        [Fact]
        public async Task ListLastTransactions_Pass()
        {
            var result = await _client
                .ListLastTransactionsAsync();
        }

        [Fact]
        public async Task SendEuroPaymentOrder_Pass()
        {
            var paymentOrders = new [] 
            {
                new EuroTransactionOrder
                {
                }
            };

            await _client.SendTransactionOrdersAsync(paymentOrders);
        }
    }
}
