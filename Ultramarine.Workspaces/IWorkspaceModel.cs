using System;
using System.Collections.Generic;

namespace Ultramarine.Workspaces
{
    public interface IWorkspaceModel
    {
        ILogger Logger { get; }
        IProjectItemModel GetProjectItem(string path);
        List<IProjectModel> GetProjects(string expression);
    }
}
