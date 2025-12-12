namespace Project.Classes;

public class Order
{
    private static readonly List<Order> _extent = new();
    public static IReadOnlyList<Order> Extent => _extent.AsReadOnly();
    
    private DateTime _dateTime;
    private readonly List<OrderItem> _orderItems = new();

    public List<OrderItem> OrderItems => _orderItems;

    public DateTime DateTime
    {
        get => _dateTime;
        set => _dateTime = value;
    }
    
    public decimal TotalPrice => _orderItems.Sum(i => i.PriceOfUnit *  i.Quantity);

    public Order() { }

    public Order(DateTime dateTime)
    {
        DateTime = dateTime;
        _extent.Add(this);
    }
    
    public OrderItem AddProduct(Product product, int quantity, OrderItem? item = null)
        {
            if (product == null) 
                throw new ArgumentNullException(nameof(product));

            if (item == null)
            {
                foreach (var existing in _orderItems)
                {
                    if (existing.Product == product)
                        return existing; 
                }

                item = new OrderItem(product, this, quantity);
                _orderItems.Add(item);

                product.AddOrder(this, quantity, item);
                return item;
            }
            else
            {
                if (!_orderItems.Contains(item))
                {
                    _orderItems.Add(item);
                    product.AddOrder(this, quantity, item);
                }

                return item;
            }
        }

        public void RemoveProduct(Product product, OrderItem? item = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            
            if (item != null)
            {
                _orderItems.Remove(item);
                return;
            }
            
            OrderItem? found = null;
            foreach (var oi in _orderItems)
            {
                if (oi.Product == product)
                {
                    found = oi;
                    break;
                }
            }

            if (found == null) return;

            _orderItems.Remove(found);
            
            product.RemoveOrder(this, found);
        }
    
    public static void LoadExtent(List<Order>? orders)
    {
        _extent.Clear();
        if (orders != null)
            _extent.AddRange(orders);
    }
}