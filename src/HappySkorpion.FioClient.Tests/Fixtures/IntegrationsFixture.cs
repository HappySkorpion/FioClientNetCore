using HappySkorpion.FioClient.Tests.Fixtures;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace HappySkorpion.FioClient.Tests
{
    [Collection(nameof(IntegrationConfigurationFixture))]
    public class IntegrationsFixture
    {
        public IntegrationsFixture(
            IntegrationConfigurationFixture integrationConfigurationFixture)
        {
            integrationConfigurationFixture.Configuration.Bind(this);
        }

        public string Token { get; }

        public DomesticTransactionOrder[] DomesticTransactionOrders { get; set; }
    }
}
