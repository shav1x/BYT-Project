using System.Text.Json;
using System.Text.Json.Serialization;
using Project.Classes;

namespace Project.Extent
{
    public static class ExtentManager
    {
        private static string _fileName = "Extent/extent.json";
        public static string FileName => _fileName;
        
        public static void SetFileNameForTesting(string path)
        {
            _fileName = path;
        }

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
                StaffMembers = Staff.Extent.ToList(),
                Movies = Movie.Extent.ToList(),
                Genres = Genre.Extent.ToList()
            };

            var json = JsonSerializer.Serialize(root, _options);
            File.WriteAllText(_fileName, json);
        }

        public static void LoadAll()
        {
            if (!File.Exists(_fileName))
                return;

            var json = File.ReadAllText(_fileName);
            if (string.IsNullOrWhiteSpace(json))
                return;

            var root = JsonSerializer.Deserialize<ExtentRoot>(json, _options);
            if (root is null)
                return;

            Customer.LoadExtent(root.Customers);
            Staff.LoadExtent(root.StaffMembers);
            Movie.LoadExtent(root.Movies);
            Genre.LoadExtent(root.Genres);
        }
    }
}