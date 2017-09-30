﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using PracaMagisterska.WPF.Exceptions;

namespace PracaMagisterska.WPF.Utils {
    public class CompilationHelper {
        #region Defaults

        /// <summary>
        /// Default namespaces usually added to project.
        /// </summary>
        public static readonly IEnumerable<string> DefaultNamespaces = new[] {
            "System",
            "System.IO",
            "System.Linq",
            "System.Text",
            "System.Collections.Generic"
        };

        /// <summary>
        /// Default assemblies usually added to project.
        /// </summary>
        public static readonly IEnumerable<MetadataReference> DefaultReferences = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IEnumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IQueryable<>).Assembly.Location)
        };

        /// <summary>
        /// Default compilation option. Usually used to compile project
        /// </summary>
        public static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.ConsoleApplication)
                .WithOptimizationLevel(OptimizationLevel.Release);

        #endregion Defaults

        #region Helpers function

        /// <summary>
        /// Helper method, which compiles <paramref name="syntaxTree"/> to <see cref="Assembly"/>
        /// </summary>
        /// <param name="syntaxTree"><see cref="SyntaxTree"/> of code to compile</param>
        /// <param name="additionalNamespace">Additional namespaces used in project</param>
        /// <param name="additionalReferences">Additional Assemblies used in project</param>
        /// <param name="compilationOptions">Specyfic compilation option used in project</param>
        /// <returns></returns>
        public static Assembly Compile(SyntaxTree syntaxTree,
                                       IEnumerable<string> additionalNamespace = null,
                                       IEnumerable<MetadataReference> additionalReferences = null,
                                       CSharpCompilationOptions compilationOptions = null) {
            // Preparation
            var allNamespace = additionalNamespace == null
                                   ? DefaultNamespaces
                                   : DefaultNamespaces.Concat(additionalNamespace);

            var allReferences = additionalReferences == null
                                    ? DefaultReferences
                                    : DefaultReferences.Concat(additionalReferences);

            var properCompilationOptions = compilationOptions == null
                                               ? DefaultCompilationOptions.WithUsings(allNamespace)
                                               : compilationOptions;

            CSharpCompilation compilation = CSharpCompilation.Create("Code")
                                                             .WithOptions(properCompilationOptions)
                                                             .AddSyntaxTrees(syntaxTree);
            foreach ( MetadataReference reference in allReferences )
                compilation = compilation.AddReferences(reference);

            using ( var ms = new MemoryStream() ) {
                // Compilation to memory
                EmitResult result = compilation.Emit(ms);

                if ( !result.Success ) // Builds failed
                    throw new CompilationException("Compilation Faliure",
                                                   result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error));
                else {
                    // Build successed 
                    ms.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Helper function, which runs default EntryPoint of assembly with strings params
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="parameters">Parameters of entry method</param>
        public static void RunMain(Assembly assembly, string[] parameters = null) {
            Run(assembly, parameters: parameters == null ? new object[] {new string[] { }} : new object[] {parameters});
        }

        /// <summary>
        /// Helper function, which runs specified method of assembly with params
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="entryPoint">Method to run</param>
        /// <param name="parameters">Parameters of entry method</param>
        public static void Run(Assembly assembly, MethodInfo entryPoint = null, object[] parameters = null) {
            if ( entryPoint == null )
                entryPoint = assembly.EntryPoint;

            entryPoint.Invoke(null, parameters);
        }

        #endregion Helpers function
    }
}