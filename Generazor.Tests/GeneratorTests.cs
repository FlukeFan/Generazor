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

                await new Generator().GenerateFileAsync(model, "dir/test.txt");

                File.ReadAllText("dir/test.txt").Should().Be("Value=value");
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
                    FileGenerator.StreamingFile(new Example2 { Value = "123" }, "dir/test.txt"),
                });

                File.ReadAllText("dir/test.txt").Should().Be("Value=123");
            }
        }

        [Test]
        public async Task GenerateFiles_StreamingFile_OverwritesExistingContent()
        {
            using (var setup = new SetupTestFolder())
            {
                File.WriteAllText("test.txt", "previous content");

                await new Generator().GenerateFilesAsync(new List<FileGenerator>
                {
                    FileGenerator.StreamingFile(new Example2 { Value = "234" }, "test.txt"),
                });

                File.ReadAllText("test.txt").Should().Be("Value=234");
            }
        }

        [Test]
        public async Task GenerateFiles_LazyFile_CreatesFile()
        {
            using (var setup = new SetupTestFolder())
            {
                await new Generator().GenerateFilesAsync(new List<FileGenerator>
                {
                    FileGenerator.LazyFile(new Example2 { Value = "567" }, "dir/test.txt"),
                });

                File.ReadAllText("dir/test.txt").Should().Be("Value=567");
            }
        }

        [Test]
        public async Task GenerateFiles_LazyFile_OverwritesIfContentIsDifferent()
        {
            using (var setup = new SetupTestFolder())
            {
                Directory.CreateDirectory("dir");
                File.WriteAllText("dir/test.txt", "previous content");

                await new Generator().GenerateFilesAsync(new List<FileGenerator>
                {
                    FileGenerator.LazyFile(new Example2 { Value = "345" }, "dir/test.txt"),
                });

                File.ReadAllText("dir/test.txt").Should().Be("Value=345");
            }
        }

        [Test]
        public async Task GenerateFiles_LazyFile_LeavesFileUntouchedIfContentUnchaged()
        {
            using (var setup = new SetupTestFolder())
            {
                File.WriteAllText("test.txt", "Value=456");
                var previousTime = File.GetLastWriteTimeUtc("test.txt");

                await new Generator().GenerateFilesAsync(new List<FileGenerator>
                {
                    FileGenerator.LazyFile(new Example2 { Value = "456" }, "test.txt"),
                });

                File.ReadAllText("test.txt").Should().Be("Value=456");
                File.GetLastWriteTimeUtc("test.txt").Should().Be(previousTime);
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
