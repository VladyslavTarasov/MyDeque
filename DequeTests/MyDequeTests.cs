using System.Collections;
using NUnit.Framework;
using Deque;

namespace DequeTests;

[TestFixture]
public class MyDequeTests
{
    [TestCase(new[] { 1, 2, 3 })]
    [TestCase(new[] { 1, 2 })]
    [TestCase(new[] { 1 })]
    public void Constructor_WithCollection_CreatesCorrectDeque(int[]? expected)
    {
        var actualDeque = new Deque<int>(expected);
        
        CollectionAssert.AreEqual(expected, actualDeque, 
            "Collections must be equal.");
    }

    [Test]
    public void Constructor_CreatesEmptyDeque()
    {
        var deque = new Deque<int>();
        
        var actualHead = deque.Head;
        var actualTail = deque.Tail;
        var actualSize = deque.Count;
        
        Assert.Multiple(() =>
        {
            Assert.That(actualHead, Is.EqualTo(null), 
                "There must be no head in an empty deque.");
            Assert.That(actualTail, Is.EqualTo(null), 
                "There must be no tail in an empty deque.");
            Assert.That(actualSize, Is.EqualTo(0), 
                "The size of an empty deque must be 0.");
        });
    }

    [Test]
    public void Constructor_WhenCollectionIsNull_ThrowsArgumentNullException()
    {
        string[]? collection = null;
        var expectedException = typeof(ArgumentNullException);

        var actualException = Assert.Catch(() => new Deque<string>(collection));
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentNullException when collection is null.");
    }

    [Test]
    public void Data_WhenNull_ThrowsArgumentNullException()
    {
        var expectedException = typeof(ArgumentNullException);

        var actualException = Assert.Catch(() => new Item<string?>(null));
        
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentNullException when Data of an Item is null.");
    }

    [TestCase(new[] { 1, 2, 3 })]
    [TestCase(new[] { 1, 2 })]
    [TestCase(new[] { 1 })]
    public void CopyTo_CopiesToArray_Correctly(int[]? expected)
    {
        var deque = new Deque<int>(expected);
        var length = deque.Count;
        var actual = new int[length];
        
        deque.CopyTo(actual, 0);

        CollectionAssert.AreEqual(expected, actual, 
            "Collections must be equal.");
    }

    [Test]
    public void CopyTo_WhenArrayIsNull_ThrowsArgumentNullException()
    {
        var dequeCollection = new[] { 1, 2 };
        var deque = new Deque<int>(dequeCollection);

        int[]? collection = null;
        var expectedException = typeof(ArgumentNullException);
        
        var actualException = Assert.Catch(() => deque.CopyTo(collection!,0));
       
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentNullException when the collection is null.");
    }

    [Test]
    public void CopyTo_WhenIndexIsLessThenZero_ThrowsArgumentOutOfRangeException_()
    {
        var dequeCollection = new[] { 1, 2 };
        var deque = new Deque<int>(dequeCollection);

        var collection = new int[2];
        var expectedException = typeof(ArgumentOutOfRangeException);
        
        var actualException = Assert.Catch(() => deque.CopyTo(collection!,-1));
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentOutOfRangeException when the index is less then 0.");
    }

    [Test]
    public void CopyTo_WhenCopyingToSmallArray_ThrowsArgumentException_()
    {
        var dequeCollection = new[] { 1, 2 };
        var deque = new Deque<int>(dequeCollection);

        var collection = new int[1];
        var expectedException = typeof(ArgumentException);
        
        var actualException = Assert.Catch(() => deque.CopyTo(collection!,0));
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentException when the deque is bigger then the array.");
    }

    [Test]
    public void CopyTo_WhenCopyingFromEmptyDeque_ThrowsInvalidOperationException()
    {
        var deque = new Deque<int>();
        var collection = new int[1];
        var expectedException = typeof(InvalidOperationException);
        
        var actualException = Assert.Catch(() => deque.CopyTo(collection!,0));
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw InvalidOperationException when trying to copy from an empty deque.");
        
    }

    [TestCase(new[] { 1, 2, 3 })]
    [TestCase(new[] { 1, 2 })]
    [TestCase(new[] { 1 })]
    public void AddFirst_AddsElements(int[]? expected)
    {
        var actualDeque = new Deque<int>();
        foreach (var i in expected!)
        {
            actualDeque.AddFirst(i);
        }

        expected = expected.Reverse().ToArray();
        
        CollectionAssert.AreEqual(expected, actualDeque, 
            "Collections must be equal.");
    }

    [TestCase(3, new[] { 1, 2, 3 })]
    [TestCase(2, new[] { 1, 2 })]
    [TestCase(1, new[] { 1 })]
    public void PeekLast_PeeksLastElement(int expectedElement, int[]? array)
    {
        var deque = new Deque<int>(array);
        var actualElement = deque.PeekLast();
        
        Assert.That(actualElement, Is.EqualTo(expectedElement), 
            "The elements must be the same.");
    }

    [Test]
    public void PeekLast_WhenPeekingFromEmptyDeque_ThrowsInvalidOperationException()
    {
        var deque = new Deque<int>();
        var expectedException = typeof(InvalidOperationException);
        
        var actualException = Assert.Catch(() => deque.PeekLast());
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw InvalidOperationException when trying to peek from an empty deque.");
    }
    
    [TestCase(3, new[] { 3, 2, 1, 0 })]
    [TestCase(2, new[] { 2, 1, 0 })]
    [TestCase(1, new[] { 1, 0 })]
    public void PeekFirst_PeeksFirstElement(int expectedElement, int[]? array)
    {
        var deque = new Deque<int>(array);
        var actualElement = deque.PeekFirst();
        
        Assert.That(actualElement, Is.EqualTo(expectedElement), 
            "The elements must be the same.");
    }
    
    [Test]
    public void PeekFirst_WhenPeekingFromEmptyDeque_ThrowsInvalidOperationException_()
    {
        var deque = new Deque<int>();
        var expectedException = typeof(InvalidOperationException);
        
        var actualException = Assert.Catch(() => deque.PeekFirst());
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw InvalidOperationException when trying to peek from an empty deque.");
    }
    
    [TestCase(3, new[] { 3, 2, 1, 0 })]
    [TestCase(2, new[] { 2, 1, 0 })]
    [TestCase(1, new[] { 1, 0 })]
    public void RemoveLast_RemovesElements(int toRemove, int[]? array)
    {
        var deque = new Deque<int>(array);
        var expected = toRemove;

        for (int i = 0; i < toRemove; i++)
        {
            deque.RemoveLast();
        }

        var actual = deque.PeekFirst();

        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(new[] { 0, 1, 2, 3 })]
    [TestCase(new[] { 0, 1, 2 })]
    [TestCase(new[] { 0, 1 })]
    public void RemoveLast_ReturnsRemovedElement(int[]? array)
    {
        var deque = new Deque<int>(array);
        var expected = array!.Last();
        
        var actual = deque.RemoveLast();
        
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RemoveLast_WhenRemovingFromEmptyDeque_ThrowsInvalidOperationException_()
    {
        var deque = new Deque<int>();
        var expectedException = typeof(InvalidOperationException);
        
        var actualException = Assert.Catch(() => deque.RemoveLast());
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw InvalidOperationException when trying to remove from an empty deque.");
    }

    [Test]
    public void RemoveLast_RemovesSingleElement()
    {
        var deque = new Deque<int>();
        deque.AddFirst(1);
        
        deque.RemoveLast();

        var headIsNull = deque.Head == null;
        var tailIsNull = deque.Tail == null;
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(headIsNull);
            Assert.IsTrue(tailIsNull);
        });
    }
    
    [TestCase(3, new[] { 0, 1, 2, 3 })]
    [TestCase(2, new[] { 0, 1, 2 })]
    [TestCase(1, new[] { 0, 1 })]
    public void RemoveFirst_RemovesElements(int toRemove, int[]? array)
    {
        var deque = new Deque<int>(array);
        var expected = toRemove;

        for (int i = 0; i < toRemove; i++)
        {
            deque.RemoveFirst();
        }

        var actual = deque.PeekLast();

        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(new[] { 3, 2, 1, 0 })]
    [TestCase(new[] { 2, 1, 0 })]
    [TestCase(new[] { 1, 0 })]
    public void RemoveFirst_ReturnsRemovedElement(int[]? array)
    {
        var deque = new Deque<int>(array);
        var expected = array!.First();
        
        var actual = deque.RemoveFirst();
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void RemoveFirst_WhenRemovingFromEmptyDeque_ThrowsInvalidOperationException_()
    {
        var deque = new Deque<int>();
        var expectedException = typeof(InvalidOperationException);
        
        var actualException = Assert.Catch(() => deque.RemoveFirst());
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw InvalidOperationException when trying to remove from an empty deque.");
    }
    
    [Test]
    public void RemoveFirst_RemovesSingleElement()
    {
        var deque = new Deque<int>();
        deque.AddFirst(1);
        
        deque.RemoveFirst();

        var headIsNull = deque.Head == null;
        var tailIsNull = deque.Tail == null;
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(headIsNull);
            Assert.IsTrue(tailIsNull);
        });
    }

    [Test]
    public void Clear_ClearsCollection()
    {
        var collection = new[]{1, 2, 3};
        var deque = new Deque<int>(collection);
        deque.Clear();
        
        Assert.Multiple(() =>
        {
            Assert.That(deque.Count, Is.EqualTo(0));
            Assert.IsNull(deque.Head);
            Assert.IsNull(deque.Tail);
        });
    }

    [Test]
    public void Contains_WhenContains_ShouldBeTrue()
    {
        var array = new[] { 1, 2, 3, 4 };
        var deque = new Deque<int>(array);

        var isContains = deque.Contains(3);
        
        Assert.IsTrue(isContains);
    }
    
    [Test]
    public void Contains_WhenNotContains_ShouldBeFalse()
    {
        var array = new[] { 1, 2, 3, 4 };
        var deque = new Deque<int>(array);

        var isContains = deque.Contains(5);
        
        Assert.IsFalse(isContains);
    }

    [Test]
    public void IsSynchronized_ShouldBeFalse()
    {
        var deque = new Deque<int>();

        var isSynchronized = ((ICollection)deque).IsSynchronized;
        
        Assert.IsFalse(isSynchronized);
    }

    [Test]
    public void SyncRoot_ShouldReturnThis()
    {
        var deque = new Deque<int>();

        var syncRoot = ((ICollection)deque).SyncRoot;
        
        Assert.IsTrue(syncRoot == deque);
    }
    
    [Test]
    public void AddLast_Event_SuccessfulAdding()
    {
        var testElement = 3;
        string? actualMessage = null;
        
        var deque = new Deque<int>();
        deque.SuccessfulAdding += delegate(object? _, DequeArgs args)
        {
            actualMessage = args.Message;
        };
        
        deque.AddLast(testElement);
        Assert.That(actualMessage, Is.EqualTo($"The element with the data {testElement} " +
                        $"was successfully added to the end."));
    }

    [Test]
    public void AddFirst_Event_SuccessfulAdding()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.SuccessfulAdding += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.AddFirst(testElement);
        Assert.That(actualMessage, Is.EqualTo($"The element with the data {testElement} " +
                                              $"was successfully added to the beginning."));
    }

    [Test]
    public void RemoveLast_Event_SuccessfulDeleting()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.AddFirst(testElement);
        deque.AddFirst(testElement + 10);
        deque.SuccessfulDeleting += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.RemoveLast();
        Assert.That(actualMessage, Is.EqualTo($"The last element with the data {testElement} " +
                                              $"was successfully deleted."));
    }
    
    [Test]
    public void RemoveFirst_Event_SuccessfulDeleting()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.AddLast(testElement);
        deque.AddLast(testElement + 10);
        deque.SuccessfulDeleting += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.RemoveFirst();
        Assert.That(actualMessage, Is.EqualTo($"The first element with the data {testElement} " +
                                              $"was successfully deleted."));
    }
    
    [Test]
    public void RemoveLast_Event_SuccessfulClearing()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.AddLast(testElement);
        deque.SuccessfulClearing += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.RemoveLast();
        Assert.That(actualMessage, Is.EqualTo("The deque is successfully cleared."));
    }
    
    [Test]
    public void RemoveFirst_Event_SuccessfulClearing()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.AddLast(testElement);
        deque.SuccessfulClearing += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.RemoveFirst();
        Assert.That(actualMessage, Is.EqualTo("The deque is successfully cleared."));
    }

    [Test]
    public void Clear_Event_SuccessfulClearing()
    {
        var testElement = 3;
        string? actualMessage = null;

        var deque = new Deque<int>();
        deque.AddLast(testElement);
        deque.SuccessfulClearing += delegate(object? _, DequeArgs args) { actualMessage = args.Message; };

        deque.Clear();
        Assert.That(actualMessage, Is.EqualTo("The deque is successfully cleared."));
    }
}