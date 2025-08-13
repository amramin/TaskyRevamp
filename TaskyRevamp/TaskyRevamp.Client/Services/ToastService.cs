namespace TaskyRevamp.Client.Services;

public class ToastService
{
    public event Func<string, string, Task> OnShow;

    public async Task ShowToastAsync(string message, string type = "info")
    {
        if (OnShow != null)
        {
            await OnShow.Invoke(message, type);
        }
    }
}