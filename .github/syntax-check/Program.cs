
using System.Text.Json;

var jsonOptions = new JsonDocumentOptions()
{
	AllowTrailingCommas = true,
	CommentHandling = JsonCommentHandling.Skip,
};

bool allParsed = true;

foreach (string jsonFile in Directory.EnumerateFiles(path: ".", searchPattern: "*.json"))
{
	string niceName = Path.GetFileName(jsonFile);

	Console.WriteLine($"Checking {niceName}...");

	using var fileStream = File.OpenRead(jsonFile);

	try
	{
		var doc = JsonDocument.Parse(fileStream, jsonOptions);
	}
	catch(JsonException ex)
	{
		allParsed = false;
		Console.WriteLine($"Error: {niceName}: {ex.Message}");
		// add a GitHub Actions annotation
		Console.WriteLine($"::error file={jsonFile},line={ex.LineNumber},endLine={ex.LineNumber},title=Json parse error::{ex.Message}");
	}
}

return allParsed ? 0 : 1;