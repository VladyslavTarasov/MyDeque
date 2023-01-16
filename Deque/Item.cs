namespace Deque;

public class Item<T>
{
    private T _data = default!;
    public Item(T data)
    {
        Data = data;
    }

    public T Data
    {
        get => _data;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                _data = value;
            }
        }
    }
    public Item<T>? Next { get; set; }
    public Item<T>? Previous { get; set; }
}