using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ultramarine.QueryLanguage;
using Ultramarine.Workspaces;

namespace Ultramarine.Roslyn
{
    public class ProjectModel : IProjectModel
    {
        private Solution _solution;
        private Project _project;
        private readonly ILogger _logger;
        public ProjectModel(string solutionPath, string projectPath, ILogger logger)
        {
            _logger = logger;
            //TODO: refactor
            using(var workspace = MSBuildWorkspace.Create())
            {
                _solution = workspace.OpenSolutionAsync(solutionPath).Result;
                _project = _solution.Projects.First(p => p.FilePath == projectPath);
                FilePath = _project.FilePath;
                Name = _project.Name;
                Language = _project.Language;

                foreach (var diag in workspace.Diagnostics)
                {
                    _logger.Warn(diag.Message);
                }
            }
        }

        public ProjectModel(Project project, ILogger logger)
        {
            _logger = logger;
            _project = project;
            _solution = project.Solution;
            FilePath = _project.FilePath;
            Name = _project.Name;
            Language = _project.Language;
        }

        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<IProjectItemModel> ProjectItems { get; set; }
        public ILogger Logger => _logger;

        public bool Build(string configuration)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel CreateDirectory(string folderPath)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel CreateProjectItem(string path, string content, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel CreateProjectItem(string path, MemoryStream content, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel CreateProjectItem(string path, byte[] content, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel CreateProjectItem(string path, object content, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public IProjectItemModel GetProjectItem(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProjectItemModel> GetProjectItems(string expression, string propertyName = "Name")
        {
            var result = new List<IProjectItemModel>();
            foreach (var item in _project.Documents)
            {
                var condition = new ConditionCompiler(expression, item.Name);
                if (condition.Execute())
                    result.Add(new ProjectItemModel(this, item, _logger));
            }

            return result;
        }

        public IEnumerable<IProjectItemModel> GetProjectItems(string expression, string dependentUpon, string propertyName = "Name")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProjectModel> GetProjects(string expression)
        {
            var result = new List<IProjectModel>();
            foreach (var project in _solution.Projects)
            {
                var condition = new ConditionCompiler(expression, project.Name);
                if (condition.Execute())
                    result.Add(new ProjectModel(project, _logger));
            }

            return result;
        }

        public IWorkspaceModel GetWorkspace()
        {
            throw new NotImplementedException();
        }

        public string ProcessTextTemplate(string t4File, object input, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
