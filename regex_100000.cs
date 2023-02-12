using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

var source = "int abs(int value)\r\n{\r\nif (value < 0)\r\n{\r\nreturn -value;\r\n}\r\nreturn value;\r\n}\r\n\r\nint i = 114;\r\nprint(abs(i - 514)); ";

//将100000个source连接
StringBuilder stringBuilder = new StringBuilder();
for (int i = 0; i < 100000; i++)
{
    stringBuilder.Append(source);
}
source = stringBuilder.ToString();

Regex rx = new(@"(?<If>\bif\b)|(?<Return>\breturn\b)|(?<LeftParen>\()|(?<RightParen>\))|(?<AssignmentOperator>\=)|(?<Minus>\-)|(?<Plus>\+)|(?<Multiply>\*)|(?<Divide>/)|(?<LeftAngularBracket>\<)|(?<RightAngularBracket>\>)|(?<EOL>;)|(?<Identifier>\b([A-Z_a-z][A-Z_a-z0-9]*)\b)|(?<Integer>\b(0|[1-9][0-9]*)\b)|(?<LeftOpenBrace>{)|(?<RightOpenBrace>})");

// 开始计时
var sw = new Stopwatch();
sw.Start();

MatchCollection matches = rx.Matches(source);

List<Group> groups = new List<Group>();

foreach (Match match in matches)
{
    var group = match.Groups.Values.Where(g => !int.TryParse(g.Name, out _) && g.Success).First();
    groups.Add(group);
}

// 结束计时
sw.Stop();
Console.WriteLine($"elapsed: {sw.ElapsedMilliseconds} ms");
Console.WriteLine($"{groups.Count} matches found.\n");