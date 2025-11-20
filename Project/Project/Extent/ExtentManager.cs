using System.Text.Json;
using System.Text.Json.Serialization;
using Project.Classes;

namespace Project.Extent;

public static class ExtentManager
{
    private const string FileName = "Extent/extent.json";

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true
    };
    
    public static void SaveAll()
    {
        var root = new ExtentRoot
        {
            Customers = Customer.Extent.ToList(),
        };

        var json = JsonSerializer.Serialize(root, _options);
        File.WriteAllText(FileName, json);
    }
    
    public static void LoadAll()
    {
        if (!File.Exists(FileName))
            return;

        var json = File.ReadAllText(FileName);
        var root = JsonSerializer.Deserialize<ExtentRoot>(json, _options);

        if (root is null)
            return;

        Customer.LoadExtent(root.Customers);
    }
}