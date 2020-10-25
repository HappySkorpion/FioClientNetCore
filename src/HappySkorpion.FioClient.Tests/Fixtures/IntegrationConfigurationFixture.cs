using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace HappySkorpion.FioClient.Tests.Fixtures
{
    public class IntegrationConfigurationFixture
    {
        public IConfigurationRoot Configuration { get; private set; }

        public IntegrationConfigurationFixture()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("integration.json", true)
                .Build();
        }

        public T Get<T>()
        {
            return Configuration.Get<T>();
        }
    }

    [CollectionDefinition(nameof(IntegrationConfigurationFixture))]
    public class IntegrationConfigurationFixtureCollection 
        : ICollectionFixture<IntegrationConfigurationFixture>
    { 
    }
}
