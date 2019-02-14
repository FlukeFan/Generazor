using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests.Samples
{
    [TestFixture]
    public class SQLiteDaoTests
    {
        [Test]
        public void OutputDisplaysArtists()
        {
            var results = Exec.CmdInProjectFolder("dotnet", "run", "Samples/SQLiteDao/ConsoleApp");

            results.Output.Count.Should().BeGreaterThan(10);
        }
    }
}
