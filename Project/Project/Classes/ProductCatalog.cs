using System.Text.RegularExpressions;

namespace Project.Classes;

public class ProductCatalog
{
    private static readonly List<ProductCatalog> _extent = new();
    public static IReadOnlyList<ProductCatalog> Extent => _extent.AsReadOnly();
    
    private string _name = null!;
    
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[A-Za-z0-9 ]+$"))
            {
                throw new ArgumentException("Name must contain only letters, numbers, and spaces.");
            }

            _name = value;
        }
    }
    
    public Dictionary<String, Product> Products { get; set; } = new();
    
    public ProductCatalog() { }
    
    public ProductCatalog(string name)
    {
        Name = name;
        
        _extent.Add(this);
    }
    
    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (Products.ContainsKey(product.Name))
            throw new ArgumentException("Product with the same name already exists in the catalog.");

        Products.Add(product.Name, product);

        if (product.Catalog != this)
            product.AddToProductCatalog(this);
    }

    public void RemoveProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (Products.Remove(product.Name))
        {
            if (product.Catalog == this)
                product.RemoveFromProductCatalog(this);
        }
        else
        {
            throw new ArgumentException("Product not found in this catalog.");
        }
    }
    
    public static void LoadExtent(List<ProductCatalog>? productCatalogs)
    {
        _extent.Clear();

        if (productCatalogs is null || productCatalogs.Count == 0)
            return;

        _extent.AddRange(productCatalogs);
    }
    
}