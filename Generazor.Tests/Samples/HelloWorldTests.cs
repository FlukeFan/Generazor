using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests.Samples
{
    [TestFixture]
    // fast enough to run each time [Category("Slow")]
    public class HelloWorldTests
    {
        [Test]
        public void OutputTransformsTemplate()
        {
            var results = Exec.CmdInProjectFolder("dotnet", "run", "Samples/HelloWorld");

            results.Output.Count.Should().Be(1);
            results.Output[0].Should().Be("GenerateString=Hello from generated template!");
        }
    }
}
