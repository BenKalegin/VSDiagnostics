﻿using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using VSDiagnostics.Utilities;

namespace VSDiagnostics.Diagnostics.Tests.RemoveTestSuffix
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RemoveTestSuffixAnalyzer : DiagnosticAnalyzer
    {
        private const DiagnosticSeverity Severity = DiagnosticSeverity.Warning;

        private static readonly string Category = VSDiagnosticsResources.TestsCategory;
        private static readonly string Message = VSDiagnosticsResources.RemoveTestSuffixAnalyzerMessage;
        private static readonly string Title = VSDiagnosticsResources.RemoveTestSuffixAnalyzerTitle;

        internal static DiagnosticDescriptor Rule
            => new DiagnosticDescriptor(DiagnosticId.RemoveTestSuffix, Title, Message, Category, Severity, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
            
        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var method = (MethodDeclarationSyntax) context.Node;
            
            if (!method.Identifier.Text.EndsWith("Test", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (!method.HasTestAttribute())
            {
                return;
            }

            context.ReportDiagnostic(Diagnostic.Create(Rule, method.Identifier.GetLocation(), method.Identifier.Text));
        }
    }
}