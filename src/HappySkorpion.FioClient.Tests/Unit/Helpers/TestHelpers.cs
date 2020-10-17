using System.IO;
using System.Text;

namespace HappySkorpion.FioClient.Tests.Unit.Helpers
{
    public static class TestHelpers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization")]
        public static Stream ToStream(
            this string data)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            stream.Position = 0;
            return stream;
        }
    }
}
