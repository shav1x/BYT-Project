namespace Project.Classes;

public class OrderItem
{
    private static readonly List<OrderItem> _extent = new();
    public static IReadOnlyList<OrderItem> Extent => _extent.AsReadOnly();

    public Product Product { get; private set; }
    public Order Order { get; private set; }

    private decimal _priceOfUnit;
    public decimal PriceOfUnit
    {
        get => _priceOfUnit;
        set
        {
            if (value < 0)
                throw new ArgumentException("PriceOfUnit cannot be negative.");
            _priceOfUnit = value;
        }
    }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Quantity must be positive.");
            _quantity = value;
        }
    }

    public OrderItem(Product product, Order order, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Order = order ?? throw new ArgumentNullException(nameof(order));

        PriceOfUnit = product?.Price ?? 0;
        Quantity = quantity;

        _extent.Add(this);
    }

    public static void LoadExtent(List<OrderItem>? list)
    {
        _extent.Clear();
        if (list != null)
            _extent.AddRange(list);
    }
}