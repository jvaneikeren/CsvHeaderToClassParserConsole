namespace CsvHeaderToClassParserConsole;

internal class Program
{
    static void Main(string[] args)
    {
        // Validate command line args if provided.
        bool useArgs = args.Any();
        if (useArgs && args.Length != 4)
        {
            throw new Exception("USAGE: CsvHeaderToClassParserConsole.exe [CsvHeader] [ClassNamespace] [ClassName] [ClassFilePath]");
        }

        // Load the local configuration that loads available configurations
        // including User Secrets.
        var config = ConfigurationHelper.GetConfigurationWithUserSecrets<Config, Program>();

        // Read in the variables; use command line args if provided, else use the local configuration.
        string csvHeader = useArgs ? args[0] : config.CsvHeader;
        string classNamespace = useArgs ? args[1] : config.ClassNamespace;
        string className = useArgs ? args[2] : config.ClassName;
        string classFilePath = useArgs ? args[3] : config.ClassFilePath;

        // Parse the header and create the class.
        var parser = new CsvHeaderToClassParser();
        var classContents = parser.ParseCsvHeaderToClass(csvHeader, classNamespace, className);
        File.WriteAllText(classFilePath.Replace("{className}", className), classContents);


        /*
         * Here's an example usage of how you would use the resulting class to read in
         * a CSV file using CSVHelper:
         * 
			List<MyClassNameModel> models = null;
			using (var reader = new StringReader(File.ReadAllText("c:/temp/myfile.csv")))
			using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
			{
				models = csv.GetRecords<MyClassNameModel>().ToList();
			}
         */
    }
}