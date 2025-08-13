using Microsoft.AspNetCore.Components;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Client.Services;

public class PopupService
{
    // This event tells a shared popup host which component to render
    public event Action<PopupType, object>? OnShow;
    public event Action? OnHide;

    public void Show(PopupType type, object parameters)
    {
        OnShow?.Invoke(type, parameters);
    }

    public void Hide()
    {
        OnHide?.Invoke();
    }

    // Shortcuts for each popup type
    public void ShowConfirmation(string message, string buttonText = "OK", Func<bool, Task>? onConfirm = null)
    {
        Show(PopupType.Confirmation, new
        {
            IsVisible = true,
            Message = message,
            ButtonText = buttonText,
            OnConfirm = EventCallback.Factory.Create<bool>(this, async (value) =>
            {
                if (onConfirm != null) await onConfirm(value);
                Hide();
            })
        });
    }

    public void ShowInvalid(string message)
    {
        Show(PopupType.InValid, new
        {
            IsVisible = true,
            Message = message
        });
    }

    public void ShowNew(RenderFragment childContent)
    {
        Show(PopupType.New, new
        {
            Visible = true,
            ChildContent = childContent
        });
    }

    public void ShowStandard(RenderFragment childContent, string header = "")
    {
        Show(PopupType.Standard, new
        {
            Visible = true,
            ChildContent = childContent,
            HeaderText = header
        });
    }
}

