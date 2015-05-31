﻿using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoslynTester.DiagnosticResults;
using RoslynTester.Helpers;
using VSDiagnostics.Diagnostics.General.IfStatementWithoutBraces;

namespace VSDiagnostics.Test.Tests.General
{
    [TestClass]
    public class IfStatementWithoutBracesAnalyzerTests : CodeFixVerifier
    {
        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_WithoutBraces_OnSameLine_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true) Console.WriteLine(""true"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 11, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic);
            VerifyCSharpFix(original, result);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_WithoutBraces_OnSameLine_WithIntermittentComment_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true) /* comments */ Console.WriteLine(""true"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true) /* comments */
            {
                Console.WriteLine(""true"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 11, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic);
            VerifyCSharpFix(original, result);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_WithoutBraces_OnNextLine_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
                Console.WriteLine(""true"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 11, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic);
            VerifyCSharpFix(original, result);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_WithBraces_DoesNotInvokeWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
        }
    }
}";
            VerifyCSharpDiagnostic(original);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_ElseStatementWithoutBraces_OnSameLine_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else Console.WriteLine(""false"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else
            {
                Console.WriteLine(""false"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 15, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic);
            VerifyCSharpFix(original, result);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_ElseStatementWithoutBraces_OnNextLine_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else 
                Console.WriteLine(""false"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else
            {
                Console.WriteLine(""false"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 15, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic);
            VerifyCSharpFix(original, result);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_ElseStatementWithBraces_DoesNotInvokeWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else 
            {
                Console.WriteLine(""false"");
            }
        }
    }
}";
            VerifyCSharpDiagnostic(original);
        }

        [TestMethod]
        public void IfStatementWithoutBracesAnalyzer_IfAndElseWithoutBraces_InvokesWarning()
        {
            var original = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
                Console.WriteLine(""true"");
            else
                Console.WriteLine(""false"");
        }
    }
}";

            var result = @"
using System;
using System.Text;

namespace ConsoleApplication1
{
    class MyClass
    {   
        void Method()
        {
            if(true)
            {
                Console.WriteLine(""true"");
            }
            else
            {
                Console.WriteLine(""false"");
            }
        }
    }
}";

            var expectedDiagnostic = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 11, 13)
                    }
            };

            var expectedDiagnostic2 = new DiagnosticResult
            {
                Id = IfStatementWithoutBracesAnalyzer.DiagnosticId,
                Message = IfStatementWithoutBracesAnalyzer.Message,
                Severity = IfStatementWithoutBracesAnalyzer.Severity,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 13, 13)
                    }
            };

            VerifyCSharpDiagnostic(original, expectedDiagnostic, expectedDiagnostic2);
            VerifyCSharpFix(original, result);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new IfStatementWithoutBracesCodeFix();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new IfStatementWithoutBracesAnalyzer();
        }
    }
}