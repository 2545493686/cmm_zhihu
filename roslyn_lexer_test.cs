// NuGet 包依赖: Microsoft.CodeAnalysis.CSharp

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Reflection;
using System.Text;

const string CODEAN_ALYSIS_CSHARP_ASSEMBLY = "Microsoft.CodeAnalysis.CSharp";
const string LEXER_TYPE = "Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.Lexer";
const string SYNTAX_TOKEN_TYPE = "Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SyntaxToken";

string source = "int abs(int value)\r\n{\r\nif (value < 0)\r\n{\r\nreturn -value;\r\n}\r\nreturn value;\r\n}\r\n\r\nint i = 114;\r\nprint(abs(i - 514)); ";

#nullable disable

object GetLexerInstance(Type lexerType) 
    => Activator.CreateInstance(lexerType, SourceText.From(source), CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp1), true, false);

// 将100000个source连接
StringBuilder stringBuilder = new();
for (int _ = 0; _ < 100000; _++)
{
    stringBuilder.Append(source);
}
source = stringBuilder.ToString();

// 因为Roslyn包的Lexer不是Public的，所以需要反射产生实例
Type lexerType = Assembly.Load(CODEAN_ALYSIS_CSHARP_ASSEMBLY).GetType(LEXER_TYPE);
MethodInfo lexMethod = lexerType.GetMethods().Where(a => a.Name == "Lex").Skip(1).First(); // 获取Lex方法

var lexer = GetLexerInstance(lexerType);

Type tokenType = Assembly.Load(CODEAN_ALYSIS_CSHARP_ASSEMBLY).GetType(SYNTAX_TOKEN_TYPE);
var getKindTextMethod = tokenType.GetMethod("get_KindText");

// 开始计时
var sw = new Stopwatch();
sw.Start();

List<(string, string)> tokens = new();

while (true)
{
    var tokenObj = lexMethod.Invoke(lexer, new object[] { 0x0001 });
    string tokenKind = (string)getKindTextMethod.Invoke(tokenObj, Array.Empty<object>());
    
    if (tokenKind == "EndOfFileToken")
    {
        break;
    }

    tokens.Add((tokenKind, tokenObj.ToString()));
}

// 结束计时
sw.Stop();
Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"Tokens Count: {tokens.Count}");