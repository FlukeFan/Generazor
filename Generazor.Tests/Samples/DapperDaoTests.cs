using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests.Samples
{
    [TestFixture]
    public class DapperDaoTests
    {
        [Test]
        public void OutputDisplaysArtists()
        {
            var results = Exec.CmdInProjectFolder("dotnet", "run", "Samples/DapperDao");

            results.Output.Count.Should().BeGreaterThan(10);
        }
    }
}
