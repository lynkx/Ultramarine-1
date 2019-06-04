using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ultramarine.QueryLanguage;
using Ultramarine.Workspaces;

namespace Ultramarine.Roslyn
{
    public class WorkspaceModel: IWorkspaceModel
    {
        private readonly string _path;
        private readonly MSBuildWorkspace _workspace;
        private Solution _solution;
        private ILogger _logger;
        
        public WorkspaceModel(string solutionPath, ILogger logger)
        {
            _path = solutionPath;
            _workspace = MSBuildWorkspace.Create();
            _logger = logger;
            Initialize();
            
        }
        public ILogger Logger => _logger;
        private async Task Initialize()
        {
            _solution = await _workspace.OpenSolutionAsync(_path);
        }       

        public IProjectItemModel GetProjectItem(string path)
        {
            throw new NotImplementedException();
        }

        public List<IProjectModel> GetProjects(string expression)
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
    }
}
