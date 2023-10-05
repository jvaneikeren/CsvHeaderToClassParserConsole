# CsvHeaderToClassParserConsole

Command-line console application to take an input CSV Header string and output a C# class file with an object model matching the CSV Header. You can specify paramters as command line arguments:

```
USAGE: CsvHeaderToClassParserConsole.exe [CsvHeader] [ClassNamespace] [ClassName] [ClassFilePath]
```
...OR you can edit the User Secrets of the project (corresponding to the Config class) and set arguments in the project's User Secrets file.


Here's an example usage of how you would use the resulting class to read in a CSV file using [CsvHelper](https://joshclose.github.io/CsvHelper/):

```
List<MyClassNameModel> models = null;
using (var reader = new StringReader(File.ReadAllText("c:/temp/myfile.csv")))
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
{
    models = csv.GetRecords<MyClassNameModel>().ToList();
}
```
         
