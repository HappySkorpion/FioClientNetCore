using System;
using System.Collections.Generic;
using System.Text;

namespace HappySkorpion.FioClient.Tests
{
    public class TokenFixture
    {
        public TokenFixture()
        {
            Token = Environment.GetEnvironmentVariable("FIO_TOKEN");
        }

        public string Token { get; }
    }
}
