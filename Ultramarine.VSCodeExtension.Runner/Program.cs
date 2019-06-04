using CommandLine;
using Microsoft.Build.Locator;
using System;
using Ultramarine.Generators.Serialization.Providers;
using Ultramarine.Roslyn;

namespace Ultramarine.VSCodeExtension.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            MSBuildLocator.RegisterDefaults();
            var logger = new StdioLogger();

            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                var generator = GeneratorSerializer.Instance.Load(o.GeneratorPath);
                generator.SetExecutionContext(new ProjectModel(o.SolutionPath, o.ProjectPath, logger));
                generator.SetLogger(logger);
                generator.Execute();
            });

            Console.ReadLine();
        }
    }
}
