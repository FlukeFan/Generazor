using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public async Task GenerateToString()
        {
            var model = new ExampleModel { Value1 = "value_1" };

            var output = await new Generator().GenerateStringAsync("/Example.cshtml", model);

            output.Should().Be("Value1=value_1");
        }
    }
}
