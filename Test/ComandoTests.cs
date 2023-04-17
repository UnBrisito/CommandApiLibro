using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandsApi.Models;

namespace Test
{
    public class ComandoTests : IDisposable
    {

        Comando testComando;
        public ComandoTests()
        {
            testComando = new Comando
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        public void Dispose()
        {
            testComando = null;
        }
        [Fact]
        public void CanChangeHowTo()
        {
            testComando.HowTo = "Execute Unit Tests";

            Assert.Equal("Execute Unit Tests", testComando.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            testComando.Platform = "asdasd";

            Assert.Equal("asdasd", testComando.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            testComando.CommandLine = "asdasd2";

            Assert.Equal("asdasd2", testComando.CommandLine);
        }
    }
}
