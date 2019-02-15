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
        public async Task GenerateToFile()
        {
            using (var setup = new SetupTestFolder())
            {
                var model = new Example2 { Value = "value" };

                await new Generator().GenerateFileAsync(model, "test.txt");

                File.ReadAllText("test.txt").Should().Be("Value=value");
            }
        }

        [Test]
        public async Task GenerateFiles_NoFiles()
        {
            using (var setup = new SetupTestFolder())
            {
                await new Generator().GenerateFilesAsync(new List<FileGenerator>());

                Directory.GetFiles(setup.TestFolder).Length.Should().Be(0);
            }
        }

        [Test]
        public async Task GenerateFiles_StreamingFile()
        {
            using (var setup = new SetupTestFolder())
            {
                await new Generator().GenerateFilesAsync(new List<FileGenerator>
                {
                    FileGenerator.StreamingFile(new Example2 { Value = "123" }, "test.txt"),
                });

                File.ReadAllText("test.txt").Should().Be("Value=123");
            }
        }

        public class SetupTestFolder : IDisposable
        {
            private string _previousCurrentDirectory;

            public SetupTestFolder()
            {
                _previousCurrentDirectory = Environment.CurrentDirectory;
                var testDir = Path.GetDirectoryName(this.GetType().Assembly.Location);
                TestFolder = Path.Combine(testDir, "_testFiles");
                DeleteFolder(TestFolder);
                Directory.CreateDirectory(TestFolder);
                Environment.CurrentDirectory = TestFolder;
            }

            public string TestFolder { get; private set; }

            public void Dispose()
            {
                Environment.CurrentDirectory = _previousCurrentDirectory;
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
}
