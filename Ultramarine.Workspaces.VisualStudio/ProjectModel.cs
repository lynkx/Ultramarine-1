﻿using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ultramarine.QueryLanguage.Comparers;

namespace Ultramarine.Workspaces.VisualStudio
{
    public class ProjectModel : IProjectModel
    {
        private readonly Project _project;
        public ProjectModel(Project project)
        {
            FilePath = project.Properties.Item("FullPath").Value.ToString();
            Name = project.Name;
            Language = project.CodeModel.Language;
            ProjectItems = new List<IProjectItemModel>();
            _project = project;
        }

        public ProjectModel(string projectName): this(Dte.Instance.GetProject(projectName))
        {

        }

        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<IProjectItemModel> ProjectItems { get; set; }

        public IProjectItemModel CreateDirectory(string folderPath)
        {
            var dirs = folderPath.Split(Path.DirectorySeparatorChar).ToList();
            var firstDir = dirs.FirstOrDefault();
            var projectItem = EnsureDirectoryExists(_project.ProjectItems, firstDir);
            dirs.Remove(firstDir);
            foreach (var dir in dirs)
            {
                projectItem = EnsureDirectoryExists(projectItem.ProjectItems, dir);
            }
            return new ProjectItemModel(projectItem);
        }

        public IProjectItemModel CreateProjectItem(string path, string content, bool overwrite)
        {
            if(!overwrite)
                if(File.Exists(path))
                    throw new Exception(string.Format("Failed to create project item. File '{0}' already exist on file system.", path));

            var directoryPath = Path.GetDirectoryName(path);
            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(path, content);
            var projectItem = _project.ProjectItems.AddFromFile(path);

            return new ProjectItemModel(projectItem);
        }

        private ProjectItem EnsureDirectoryExists(ProjectItems projectItems, string folderName)
        {
            ProjectItem projectItem;
            try
            {
                var projectPath = projectItems.ContainingProject.Properties.Item("FullPath").Value.ToString();
                var directoryPath = Path.Combine(projectPath, folderName);
                if (Directory.Exists(directoryPath))
                    projectItem = projectItems.AddFromDirectory(directoryPath);
                else
                    projectItem = projectItems.AddFolder(folderName);
                
            }
            catch (Exception ex)
            {
                var item = GetProjectItems(projectItems, new Comparer(folderName, OperatorType.Equals)).FirstOrDefault(); //GetProjectItems(projectItems, true, false, new Conditioner(folderName, StringComparisonType.Equal)).FirstOrDefault();
                if (item == null)
                    throw new Exception(string.Format("Unknown exception while trying to find directory {0}", folderName));
                projectItem = item;
            }
            return projectItem;
        }

        private List<ProjectItem> GetProjectItems(ProjectItems projectItems, Comparer comparer, string propertyName = null)
        {
            var result = new List<ProjectItem>();
            if (projectItems == null)
                return result;

            foreach (ProjectItem projectItem in projectItems)
            {
                var propertyValue = string.IsNullOrWhiteSpace(propertyName)
                    ? projectItem.Name
                    : GetPropertyValue(projectItem, propertyName);

                var comparisonResult = comparer.Check(propertyValue);
                if (comparisonResult)
                    result.Add(projectItem);

                var childProjectItems = projectItem.ProjectItems;
                if (childProjectItems == null)
                    continue;

                result.AddRange(GetProjectItems(childProjectItems, comparer, propertyName));
            }
            return result;
        }

        //TODO: refactor
        private string GetPropertyValue(ProjectItem item, string propertyName)
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
