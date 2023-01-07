namespace CsvHeaderToClassParserConsole;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 4)
            throw new Exception("USAGE: CsvHeaderToClassParserConsole.exe [CsvHeader] [ClassNamespace] [ClassName] [ClassFilePath]");

        string csvHeader = args[0];
        string classNamespace = args[1];
        string className = args[2];
        string classFilePath = args[3];

        var parser = new CsvHeaderToClassParser();
        var classContents = parser.ParseCsvHeaderToClass(csvHeader, classNamespace, className);
        File.WriteAllText(classFilePath, classContents);
    }
}