using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ultramarine.Workspaces.CodeElements;

namespace Ultramarine.Roslyn.CodeElements
{
    public class CodeElementModel : ICodeElementModel
    {
        private SyntaxNode _syntaxNode;

        public CodeElementModel(SyntaxNode node)
        {
            _syntaxNode = node;
            this.Name = node.GetText().ToString();
            Type = GetElementType(node);
        }

        private ElementType GetElementType(SyntaxNode node)
        {
            var nodeType = node.GetType();
            if (nodeType == typeof(ClassDeclarationSyntax))
                return ElementType.Class;
            if (nodeType == typeof(PropertyDeclarationSyntax))
                return ElementType.Property;
            if (nodeType == typeof(MethodDeclarationSyntax))
                return ElementType.Function;
            if (nodeType == typeof(EnumDeclarationSyntax))
                return ElementType.Enum;
            if (nodeType == typeof(InterfaceDeclarationSyntax))
                return ElementType.Interface;


            return ElementType.Other;
        }
        public string Name { get; set; }
        public ElementType? Type { get; set; }
        public ElementAccess? Access { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ElementOverride Override { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> TypeOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<ICodeElementModel> Children { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
