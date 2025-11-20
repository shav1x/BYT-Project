using Project.Extent;

namespace Project;

public class Program
{
    public static void Main(string[] args)
    {
        ExtentManager.LoadAll();
        
        ExtentManager.SaveAll();
    }
}