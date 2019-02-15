using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Generazor.Tests.Templates;
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

        [Test]
        public async Task GenerateToString_ViewConvention()
        {
            var model = new ExampleModel { Value1 = "value_1" };

            var output = await new Generator().GenerateStringAsync(model);

            output.Should().Be("Value1=value_1");
        }

        [Test]
        public async Task GenerateToString_SubFolder()
        {
            var model = new Example2 { Value = "value" };

            var output = await new Generator().GenerateStringAsync(model);

            output.Should().Be("Value=value");
        }

        [Test]
        public async Task GenerateFiles_NoFiles()
        {
            var folder = SetupTestFolder();

            await new Generator().GenerateFilesAsync(new List<FileGenerationInfo>());

            Directory.GetFiles(folder).Length.Should().Be(0);
        }

        private string SetupTestFolder()
        {
            var testDir = Path.GetDirectoryName(this.GetType().Assembly.Location);
            var testFolder = Path.Combine(testDir, "_testFiles");
            DeleteFolder(testFolder);
            Directory.CreateDirectory(testFolder);
            Environment.CurrentDirectory = testFolder;
            return testFolder;
        }

        private static void DeleteFolder(string folder)
        {
            var count = 3;

            while (Directory.Exists(folder))
                try { Directory.Delete(folder, true); }
                catch
                {
                    Thread.Sleep(0);

                    if (count-- == 0)
                        throw;
                }
        }
    }
}
