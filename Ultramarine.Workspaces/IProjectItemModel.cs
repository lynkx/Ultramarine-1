using System.Collections.Generic;
using Ultramarine.Workspaces.CodeElements;

namespace Ultramarine.Workspaces
{
    public interface IProjectItemModel
    {
        ILogger Logger { get; }
        string FilePath { get; set; }
        string Name { get; set; }
        string Language { get; set; }
        IProjectModel Project { get; }
        List<IProjectItemModel> ProjectItems { get; set; }
        List<IProjectItemModel> GetProjectItems(string expression);
        List<ICodeElementModel> GetCodeElements(string expression);
        string GetProperty(string propertyName = "FileName");
    }
}
