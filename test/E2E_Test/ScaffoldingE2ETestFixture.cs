using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.Cli.Utils;
using Xunit;

namespace E2E_Test
{
    public class ScaffoldingE2ETestFixture : IDisposable
    {
        const string projectJson = @"
{
  ""dependencies"": {
    ""Microsoft.NETCore.App"": {
      ""version"": ""1.1.0"",
      ""type"": ""platform""
    },
    ""Microsoft.AspNetCore.Diagnostics"": ""1.0.0"",
    ""Microsoft.AspNetCore.Mvc"": ""1.0.0"",
    ""Microsoft.AspNetCore.Server.IISIntegration"": ""1.0.0"",
    ""Microsoft.AspNetCore.Server.Kestrel"": ""1.1.0-*"",
    ""Microsoft.AspNetCore.StaticFiles"": ""1.0.0"",
    ""Microsoft.Extensions.Configuration.EnvironmentVariables"": ""1.1.0-*"",
    ""Microsoft.Extensions.Configuration.Json"": ""1.0.0"",
    ""Microsoft.Extensions.Logging"": ""1.1.0-*"",
    ""Microsoft.Extensions.Logging.Console"": ""1.0.0"",
    ""Microsoft.Extensions.Logging.Debug"": ""1.0.0"",
    ""Microsoft.Extensions.Options.ConfigurationExtensions"": ""1.0.0"",
    ""Microsoft.VisualStudio.Web.BrowserLink.Loader"": ""14.0.0"",
     ""Microsoft.VisualStudio.Web.CodeGeneration.Tools"": {
       ""version"": ""1.1.0-*"",
       ""type"": ""build"",
       ""target"": ""package""
     },
    ""Microsoft.VisualStudio.Web.CodeGenerators.Mvc"": ""1.0.0-*""
  },

   ""tools"": {
     ""Microsoft.VisualStudio.Web.CodeGeneration.Tools"": {
       ""version"": ""1.1.0-*"",
       ""imports"": [""portable-net451+win8""]
     }
  },

  ""frameworks"": {
    ""netcoreapp1.1"": {
      ""imports"": [
        ""dotnet5.6"",
        ""portable-net45+win8""
      ]
    }
  },

  ""buildOptions"": {
    ""emitEntryPoint"": true,
    ""preserveCompilationContext"": true
  },

  ""runtimeOptions"": {
    ""configProperties"": {
      ""System.GC.Server"": true
    }
  }
}
";
        public ScaffoldingE2ETestFixture()
        {
            var projectJsonPath = Path.Combine(Path.GetFullPath(@"../../TestApps/WebApplication1"), "project.json");
            InitializeProjectJson(projectJsonPath);
            FilesToCleanUp = new List<string>() {projectJsonPath};
            FoldersToCleanUp = new List<string>();
            BaseLineFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Baseline");
            TestProjectDirectory = Path.GetFullPath(@"../../TestApps/WebApplication1");
        }

        public List<string> FilesToCleanUp { get; private set; }
        public List<string> FoldersToCleanUp { get; private set; }
        public string BaseLineFilesDirectory { get; private set; }

        public string TestProjectDirectory { get; private set; }

        public void Dispose()
        {
            Console.WriteLine("Cleaning up generated files:");
            foreach(var file in FilesToCleanUp)
            {
                Console.WriteLine($"     {file}");
                File.Delete(Path.GetFullPath(file));
            }

            Console.WriteLine("Cleaning up generated folders");
            foreach(var folder in FoldersToCleanUp)
            {
                Console.WriteLine($"    {folder}");
                Directory.Delete(folder);
            }
        }

        private void InitializeProjectJson(string projectJsonPath)
        {
            File.WriteAllText(projectJsonPath, projectJson);
            new CommandFactory()
                .Create("dotnet", new string[] {"restore", projectJsonPath})
                .CaptureStdOut()
                .CaptureStdErr()
                .Execute();
        }
    }

    [CollectionDefinition("ScaffoldingE2ECollection")]
    public class ScaffoldingE2ECollection : ICollectionFixture<ScaffoldingE2ETestFixture>
    {

    }
}
