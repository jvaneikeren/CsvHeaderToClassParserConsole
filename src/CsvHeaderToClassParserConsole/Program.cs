using Orbital7.Extensions;

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
        string csvHeader = useArgs ? args[0] : config.CsvHeader ?? throw new ArgumentNullException(nameof(csvHeader));
        string classNamespace = useArgs ? args[1] : config.ClassNamespace ?? throw new ArgumentNullException(nameof(classNamespace)); ;
        string className = useArgs ? args[2] : config.ClassName ?? throw new ArgumentNullException(nameof(className)); ;
        string classFilePath = useArgs ? args[3] : config.ClassFilePath ?? throw new ArgumentNullException(nameof(classFilePath)); ;

        // Build write the class file from the CSV header.
        var builder = new CsvModelClassBuilder();
        builder.WriteClassFileFromCsvHeader(
            csvHeader, 
            classNamespace, 
            className,
            classFilePath.Replace("{className}", className));

        /*
         * Here's an example usage of how you would use the resulting class to read in
         * a CSV file using the CSVHelper package:
         * 
			List<MyClassNameModel> models = null;
			using (var reader = new StringReader(File.ReadAllText("c:/temp/myfile.csv")))
			using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
			{
				models = csv.GetRecords<MyClassNameModel>().ToList();
			}
         *
         *
         *  OR using the Orbital7.Extensions package:
         *  
            List<MyClassNameModel> models = ParsingHelper.ParseCsvFileToModels<MyClassNameModel>(
                "c:/temp/myfile.csv",
                hasColumnHeadersRow = true);
         */
    }
}