using System.Text.RegularExpressions;

namespace Project.Classes;

public class Product
{
    private static readonly List<Product> _extent = new();
    public static IReadOnlyList<Product> Extent => _extent.AsReadOnly();

    
    private string _name = null!;
    private decimal _price;
    private bool _availability;
    private string? _description;
    
    private ProductCatalog? _catalog;
    public ProductCatalog? Catalog => _catalog;
    
    private readonly List<OrderItem> _orderItems = new();

    public List<OrderItem> OrderItems => _orderItems;
    
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

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");

            _price = value;
        }
    }

    public bool Availability
    {
        get => _availability;
        set => _availability = value;
    }

    public string? Description
    {
        get => _description;
        set
        {
            if (value != null && value.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            _description = value;
        }
    }
    
    private readonly List<Product> _children = new();
    public IReadOnlyList<Product> Children => _children.AsReadOnly();

    private readonly List<Product> _parents = new();
    public IReadOnlyList<Product> Parents => _parents.AsReadOnly();
    
    public void AddChild(Product child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));
        if (child == this) throw new InvalidOperationException("Cannot reference itself.");

        if (!_children.Contains(child))
        {
            _children.Add(child);
            
            if (!child._parents.Contains(this))
                child.AddParent(this);
        }
    }

    public void AddParent(Product parent)
    {
        if (parent == null) throw new ArgumentNullException(nameof(parent));
        if (parent == this) throw new InvalidOperationException("Cannot reference itself.");

        if (!_parents.Contains(parent))
        {
            _parents.Add(parent);

            if (!parent._children.Contains(this))
                parent.AddChild(this);
        }
    }

    public void RemoveChild(Product child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));

        if (_children.Remove(child))
        {
            if (child._parents.Contains(this))
                child.RemoveParent(this);
        }
    }

    public void RemoveParent(Product parent)
    {
        if (parent == null) throw new ArgumentNullException(nameof(parent));

        if (_parents.Remove(parent))
        {
            if (parent._children.Contains(this))
                parent.RemoveChild(this);
        }
    }

    public void AddToProductCatalog(ProductCatalog catalog)
    {
        if (catalog == null) throw new ArgumentNullException(nameof(catalog));

        if (_catalog == catalog)
            return;

        if (_catalog != null)
            throw new InvalidOperationException("Product already belongs to another catalog.");

        _catalog = catalog;

        if (!catalog.Products.ContainsValue(this))
            catalog.Products.Add(Name, this);
        
        if (!catalog.Products.ContainsKey(Name))
            catalog.AddProduct(this);
    }

    public void RemoveFromProductCatalog(ProductCatalog catalog)
    {
        if (catalog == null) throw new ArgumentNullException(nameof(catalog));
        if (_catalog != catalog)
            throw new ArgumentException("This product does not belong to the given catalog.");

        _catalog = null;

        if (catalog.Products.ContainsKey(Name))
            catalog.Products.Remove(Name);
        
        if (catalog.Products.ContainsValue(this))
            catalog.RemoveProduct(this);
    }
    
    
    public OrderItem AddOrder(Order order, int quantity, OrderItem? item = null)
    {
        if (order == null) 
            throw new ArgumentNullException(nameof(order));

        if (item == null)
        {
            foreach (var existing in _orderItems)
            {
                if (existing.Order == order)
                    return existing; 
            }

            item = new OrderItem(this, order, quantity);
            _orderItems.Add(item);

            order.AddProduct(this, quantity, item);
            return item;
        }
        else
        {
            if (!_orderItems.Contains(item))
            {
                _orderItems.Add(item);
                order.AddProduct(this, quantity, item);
            }

            return item;
        }
    }

    public void RemoveOrder(Order order, OrderItem? item = null)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
            
        if (item != null)
        {
            _orderItems.Remove(item);
            return;
        }
            
        OrderItem? found = null;
        foreach (var oi in _orderItems)
        {
            if (oi.Order == order)
            {
                found = oi;
                break;
            }
        }

        if (found == null) return;

        _orderItems.Remove(found);
            
        order.RemoveProduct(this, found);
    }
    
    public Product()
    {
    }

    public Product(string name, decimal price, bool availability, string? description = null)
    {
        Name = name;
        Price = price;
        Availability = availability;
        Description = description;
        

        _extent.Add(this);
    }

    public static void LoadExtent(List<Product>? products)
    {
        _extent.Clear();

        if (products is null || products.Count == 0)
            return;

        _extent.AddRange(products);
    }
}