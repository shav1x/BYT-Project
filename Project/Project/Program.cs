using Project.Classes;
using Project.Serializers;

namespace Project;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Customer customer1 = new Customer(
                name: "Andrii",
                surname: "Doe",
                dateOfBirth: new DateTime(1990, 5, 12),
                email: "john.doe@example.com",
                phone: "123456789"
            );
            Customer customer2 = new Customer(
                name: "Bob",
                surname: "Doe",
                dateOfBirth: new DateTime(1990, 5, 12),
                email: "john.doe@example.com",
                phone: "123456789"
            );
            
            Console.WriteLine(customer1.Name);
            SerDes.SerializeToXml(customer1, "Cinema.xml");
            SerDes.SerializeToXml(customer2, "Cinema.xml");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}