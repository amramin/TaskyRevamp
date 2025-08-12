namespace TaskyRevamp.Client;

public class PopupService
{
    public event Action<string, string, Func<Task>?>? OnShow;
    public event Action? OnHide;

    public void Show(string title, string message, Func<Task>? onConfirm = null)
    {
        Console.WriteLine($"[PopupService] Showing: {title}");
        OnShow?.Invoke(title, message, onConfirm);
    }

    public void Hide()
    {
        Console.WriteLine("[PopupService] Hiding popup");
        OnHide?.Invoke();
    }
}
