using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using Ultramarine.QueryLanguage;

namespace Ultramarine.Workspaces.VisualStudio
{
    public class WorkspaceModel: IWorkspaceModel
    {
        private Solution _solution;
        private ILogger _logger;

        public WorkspaceModel(Solution solution, ILogger logger)
        {
            _solution = solution;
            _logger = logger;
        }

        public ILogger Logger => _logger;

        public IProjectItemModel GetProjectItem(string path)
        {
            var results = new List<ProjectItem>();
            foreach (Project project in _solution.Projects)
            {
                var projectItems = GetProjectItems(project.ProjectItems, "FullPath", path);
                results.AddRange(projectItems);

            }

            if (!results.Any())
                return null;

            return new ProjectItemModel(results.First(), _logger);
        }

        public List<IProjectModel> GetProjects(string expression)
        {
            var result = new List<IProjectModel>();
            foreach(Project project in _solution.Projects)
            {
                var condition = new ConditionCompiler(expression, GetProperty(project, "Name"));
                if (condition.Execute())
                    result.Add(new ProjectModel(project, _logger));
            }

            return result;
        }

        private List<ProjectItem> GetProjectItems(ProjectItems items, string propertyName, string val)
        {
            var result = new List<ProjectItem>();
            foreach(ProjectItem projectItem in items)
            {
                var propertyValue = GetProperty(projectItem, propertyName);
                if (propertyValue == val)
                    result.Add(projectItem);
            }

            return result;
        }

        private string GetProperty(Project project, string propertyName)
        {
            try
            {
                return project.Properties.Item(propertyName).Value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        private string GetProperty(ProjectItem item, string propertyName)
        {
            try
            {
                return item.Properties.Item(propertyName).Value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
