#pragma warning disable RS2008

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;
using System.Threading;

namespace UniTask.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UniTaskAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: "UNITASK001",
            title: "UniTaskAnalyzer001: Must pass CancellationToken",
            messageFormat: "Must pass CancellationToken",
            category: "Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "Pass CancellationToken or CancellationToken.None.");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterOperationAction(AnalyzeOperation, OperationKind.Invocation);
        }

        private static void AnalyzeOperation(OperationAnalysisContext context)
        {
            var token = context.Compilation.GetTypeByMetadataName(typeof(CancellationToken).FullName);
            if (token == null) return;

            if (context.Operation is IInvocationOperation invocation)
            {
                foreach (var arg in invocation.Arguments)
                {
                    if (arg.ArgumentKind == ArgumentKind.DefaultValue)
                    {
                        if (SymbolEqualityComparer.Default.Equals(arg.Parameter.Type, token))
                        {
                            var diagnostic = Diagnostic.Create(Rule, arg.Syntax.GetLocation());
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }
    }
}