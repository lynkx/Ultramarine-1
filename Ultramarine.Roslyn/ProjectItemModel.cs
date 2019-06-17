using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using Ultramarine.QueryLanguage;
using Ultramarine.Roslyn.CodeElements;
using Ultramarine.Workspaces;
using Ultramarine.Workspaces.CodeElements;

namespace Ultramarine.Roslyn
{
    public class ProjectItemModel : IProjectItemModel
    {
        private readonly ILogger _logger;
        private Document _projectItem;
        public ProjectItemModel(IProjectModel project, Document projectItem, ILogger logger)
        {
            FilePath = projectItem.FilePath;
            Name = projectItem.Name;
            Language = projectItem.Project.Language;
            Project = project;
            _logger = logger;
            _projectItem = projectItem;
        }

        public ILogger Logger => _logger;
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }

        public IProjectModel Project { get; private set; }

        public List<IProjectItemModel> ProjectItems { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public List<ICodeElementModel> GetCodeElements(string expression)
        {
            var result = new List<SyntaxNode>();
            var codeModel = _projectItem.GetSemanticModelAsync().Result; //GetCodeModel(_projectItem);
            if (codeModel == null)
                return null;

            var unit = codeModel.SyntaxTree.GetCompilationUnitRoot();

            foreach (MemberDeclarationSyntax member in unit.Members)
            {
                var memberName = member.TryGetInferredMemberName();
                if (string.IsNullOrWhiteSpace(memberName))
                {
                    result.AddRange(GetInnerCodeElements(member, expression));
                }
                else
                {
                    var condition = new ConditionCompiler(expression, memberName);
                    if (condition.Execute())
                        result.Add(member);
                    else
                        result.AddRange(GetInnerCodeElements(member, expression));
                }
            }

            return result.Select<SyntaxNode, ICodeElementModel>(c => new CodeElementModel(c)).ToList();
        }

        private List<SyntaxNode> GetInnerCodeElements(SyntaxNode member, string expression)
        {
            var result = new List<SyntaxNode>();

            foreach (var node in member.ChildNodes())
            {
                var memberName = node.TryGetInferredMemberName();
                if (string.IsNullOrWhiteSpace(memberName))
                {
                    result.AddRange(GetInnerCodeElements(node, expression));
                }
                else
                {
                    var condition = new ConditionCompiler(expression, node.TryGetInferredMemberName());
                    if (condition.Execute())
                        result.Add(node);
                    else
                        result.AddRange(GetInnerCodeElements(member, expression));
                }
            }
            return result;
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
