using System.Collections;
using Deque;

namespace MyDeque
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var arr = new[] { "One", "Two", "Three", "Four" };
            var deque = new Deque<string>(arr);

            deque.SuccessfulAdding += DisplayMessage;
            deque.SuccessfulDeleting += DisplayMessage;
            deque.SuccessfulClearing += DisplayMessage;
            
            foreach (var item in deque)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.ReadKey(true);
            
            var copyTo = new string[4];
            try
            {
                deque.CopyTo(copyTo, 1);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine();
            Console.ReadKey(true);
            
            deque.CopyTo(copyTo, 0);

            foreach (var s in copyTo)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();
            Console.ReadKey(true);
            
            deque.RemoveFirst();
            deque.RemoveLast();
            Console.WriteLine();
            foreach (var item in deque)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine();
            Console.ReadKey(true);
            
            deque.AddFirst("1000");
            deque.AddLast("0001");
            Console.WriteLine();
            foreach (var item in deque)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine();
            Console.ReadKey(true);

            Console.WriteLine("The first element is: " + deque.PeekFirst());
            Console.WriteLine("The last element is: " + deque.PeekLast());
            Console.WriteLine("This deque contains element Ten: " + deque.Contains("Ten"));
            Console.WriteLine("This deque contains element 1000: " + deque.Contains("1000"));
            
            Console.WriteLine();
            Console.ReadKey(true);

            deque.Clear();
            
            Console.WriteLine();
            Console.ReadKey(true);
            
            try
            {
                deque.RemoveFirst();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void DisplayMessage(object? sender, DequeArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}