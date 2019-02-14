using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests.Samples
{
    [TestFixture]
    public class SQLiteDALTests
    {
        [Test]
        public void OutputDisplaysArtists()
        {
            var results = Exec.CmdInProjectFolder("dotnet", "run", "Samples/SQLiteDAL/ConsoleApp");

            results.Output.Count.Should().BeGreaterThan(10);
        }
    }
}
