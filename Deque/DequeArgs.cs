namespace Deque;

public class DequeArgs : EventArgs
{
    public DequeArgs(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}