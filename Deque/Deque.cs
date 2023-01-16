using System.Collections;

namespace Deque;

public class Deque<T> : IEnumerable<T>, ICollection
{
    private Item<T>? _head;
    private Item<T>? _tail;
    private int _size;
    
    public Item<T>? Head => _head;
    public Item<T>? Tail => _tail;
    public int Count => _size;

    public event EventHandler<DequeArgs>? SuccessfulAdding;
    public event EventHandler<DequeArgs>? SuccessfulDeleting;
    public event EventHandler<DequeArgs>? SuccessfulClearing;

    public Deque()
    {
        _head = null;
        _tail = null;
        _size = 0;
    }

    public Deque(IEnumerable<T>? collection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException();
        }

        foreach (T obj in collection)
        {
            this.AddLast(obj);
        }
    }
    
    public void CopyTo(Array array, int index)
    {
        if (array is null)
        {
            throw new ArgumentNullException();
        }

        if (index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        int maxIndex = index + _size;
        if (maxIndex > array.Length)
        {
            throw new ArgumentException("Deque is too long for this array.");
        }
        
        Item<T>? curr = _head;
        if (curr is null)
        {
            throw new InvalidOperationException("Deque is empty");
        }
        
        for (var i = index; i <= maxIndex; i++)
        {
            array.SetValue(curr.Data, i);
            curr = curr.Next;
            if (curr is null)
            {
                break;                
            }
        }
    }

    public void AddLast(T data)
    {
        Item<T> item = new Item<T>(data);

        if (_head is null)
        {
            _head = item;
        }
        else
        {
            _tail!.Next = item;
            item.Previous = _tail;
        }

        _tail = item;
        _size++;
        
        SuccessfulAdding?.Invoke(this, new DequeArgs($"The element with the data {data} " +
                                                     $"was successfully added to the end."));
    }

    public void AddFirst(T data)
    {
        Item<T> item = new Item<T>(data);
        
        if (_head is null)
        {
            _tail = item;
        }
        else
        {
            _head!.Previous = item;
            item.Next = _head;
        }

        _head = item;
        _size++;
        
        SuccessfulAdding?.Invoke(this, new DequeArgs($"The element with the data {data} " +
                                                     $"was successfully added to the beginning."));
    }
    
    public T PeekLast()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        return _tail!.Data;
    }
    
    public T PeekFirst()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("Deque is empty");
        }

        return _head!.Data;
    }

    public T RemoveLast()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("Cannot delete from an empty deque.");
        }

        T data = _tail!.Data;

        if (_size == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _tail = _tail!.Previous;
            _tail!.Next = null;
        }

        _size--;
        
        SuccessfulDeleting?.Invoke(this, new DequeArgs($"The last element with the data {data} " +
                                                       $"was successfully deleted."));

        if (_size == 0)
        {
            SuccessfulClearing?.Invoke(this, new DequeArgs("The deque is successfully cleared."));
        }
        
        return data;
    }
    
    public T RemoveFirst()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("Cannot delete from an empty deque.");
        }

        T data = _head!.Data;
        
        if (_size == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head!.Next;
            _head!.Previous = null;
        }

        _size--;
        
        SuccessfulDeleting?.Invoke(this, new DequeArgs($"The first element with the data {data} " +
                                                       $"was successfully deleted."));
        
        if (_size == 0)
        {
            SuccessfulClearing?.Invoke(this, new DequeArgs("The deque is successfully cleared."));
        }

        return data;
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
        _size = 0;
        
        SuccessfulClearing?.Invoke(this, new DequeArgs("The deque is successfully cleared."));
    }

    public bool Contains(T data)
    {
        Item<T>? curr = _head;

        while (curr is not null)
        {
            if (curr.Data!.Equals(data))
            {
                return true;
            }

            curr = curr.Next;
        }
        return false;
    }

    public bool IsSynchronized => false;

    object ICollection.SyncRoot => this;
    
    public IEnumerator<T> GetEnumerator()
    {
        Item<T>? curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /*bool ICollection<T>.Remove(T item)
    {

        Item<T>? curr = _head;

        while (curr is not null)
        {
            if (curr.Data!.Equals(item))
            {
                return true;
            }

            curr = curr.Next;
            return false;
        }
    }
    
    void ICollection<T>.Add(T item)
    {
        throw new NotImplementedException();
    }
    
    void ICollection<T>.Clear()
    {
        throw new NotImplementedException();
    }

    bool ICollection<T>.IsReadOnly => false;
    
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException();
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (array.Rank > 1)
        {
            throw new RankException();
        }

        int maxIndex = arrayIndex + _size;
        if (maxIndex > array.Length)
        {
            throw new ArgumentException("Deque is too long for this array.");
        }
        
        Item<T>? curr = _head;
        if (curr is null)
        {
            throw new InvalidOperationException("Deque is empty");
        }
        
        for (var i = arrayIndex; i <= maxIndex; i++)
        {
            array.SetValue(curr.Data, i);
            curr = curr.Next;
            if (curr is null)
            {
                break;                
            }
        }
    }*/
}