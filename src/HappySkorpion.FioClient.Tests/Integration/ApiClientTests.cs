using HappySkorpion.FioClient.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Integration
{
    [Trait("Category", "Integration")]
    [Collection(nameof(IntegrationConfigurationFixture))]
    public class ApiClientTests
        : IDisposable,
        IClassFixture<IntegrationsFixture>
    {
        public class Fixture
        {
            public string Token { get; set; }
            public IList<DomesticTransactionOrder> DomesticTransactionOrders { get; set; }
        }

        private readonly Fixture _fixture;
        private readonly ApiClient _client;

        #region Initialization and Clenaup

        public ApiClientTests(
            IntegrationConfigurationFixture configurationFixture)
        {
            _fixture = configurationFixture.Get<Fixture>();
            _client = new ApiClient(_fixture.Token);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        [Fact]
        public async Task ListTransactionsInPeriod_Pass()
        {
            await _client.ListTransactionsAsync(DateTime.Today.AddDays(-1), DateTime.Today);
        }

        [Fact]
        public async Task ListTransactionsIn_Pass()
        {
            await _client.ListTransactionsAsync(2020, 1);
        }

        [Fact]
        public async Task ListLastTransactions_Pass()
        {
            await _client.ListLastTransactionsAsync();
        }

        [Fact]
        public async Task SendEuroPaymentOrder_Pass()
        {
            await _client.SendTransactionOrdersAsync(_fixture.DomesticTransactionOrders);
        }
    }
}
