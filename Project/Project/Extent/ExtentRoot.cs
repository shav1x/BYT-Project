using Project.Classes;

namespace Project.Extent;

public class ExtentRoot
{
    public List<Customer> Customers { get; set; } = new();
    public List<Staff> StaffMembers { get; set; } = new();
    public List<Movie> Movies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
}