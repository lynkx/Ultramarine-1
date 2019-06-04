using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Ultramarine.Workspaces;
using Ultramarine.Workspaces.CodeElements;

namespace Ultramarine.Roslyn
{
    public class ProjectItemModel : IProjectItemModel
    {
        private readonly ILogger _logger;
        public ProjectItemModel(IProjectModel project, Document projectItem, ILogger logger)
        {
            FilePath = projectItem.FilePath;
            Name = projectItem.Name;
            Language = projectItem.Project.Language;
            Project = project;
            _logger = logger;
        }

        public ILogger Logger => _logger;
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }

        public IProjectModel Project { get; private set; }

        public List<IProjectItemModel> ProjectItems { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public List<ICodeElementModel> GetCodeElements(string expression)
        {
            throw new System.NotImplementedException();
        }

        public List<IProjectItemModel> GetProjectItems(string expression)
        {
            throw new System.NotImplementedException();
        }

        public string GetProperty(string propertyName = "Name")
        {
            throw new System.NotImplementedException();
        }
    }
}
