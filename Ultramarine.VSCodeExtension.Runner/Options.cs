using CommandLine;

namespace Ultramarine.VSCodeExtension.Runner
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
        [Option('g', "generator", Required = true, HelpText = "Set generator config path.")]
        public string GeneratorPath { get; set; }
        [Option('p', "project", Required = true, HelpText = "Set project path")]
        public string ProjectPath { get; set; }
        [Option('s', "solution", Required = true, HelpText = "Set solution path")]
        public string SolutionPath { get; set; }
    }
}
