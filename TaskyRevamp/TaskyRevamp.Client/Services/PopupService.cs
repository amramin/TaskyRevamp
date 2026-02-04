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

    public void ShowConfirmation(
        string message,
        string buttonText = "",
        Func<bool, Task>? onConfirm = null,
        ConfirmationType type = ConfirmationType.Primary)
    {
        Show(PopupType.Confirmation, new ConfirmationPopupParams
        {
            Message = message,
            ButtonText = buttonText,
            Type = type,
            OnConfirm = EventCallback.Factory.Create<bool>(this, async (value) =>
            {
                if (onConfirm != null)
                    await onConfirm(value);
                Hide();
            })
        });
    }
    public void ShowInfo(
    string message,
    string Info,
    ConfirmationType type = ConfirmationType.Primary)
    {
        Show(PopupType.Info, new InfoPopupParams
        {
            Message = message,
            Info=Info,
            Type = type,
            OnConfirm = EventCallback.Factory.Create<bool>(this, async (value) =>
            {
                Hide();
            })
        });
    }


    public void ShowInvalid(string message)
    {
        Show(PopupType.InValid, new InvalidPopupParams
        {
            Message = message
        });
    }

    public void ShowNew(RenderFragment childContent, string headerText = "")
    {
        Show(PopupType.New, new NewPopupParams
        {
            ChildContent = childContent,
            HeaderText = headerText

        });
    }

    public void ShowStandard(RenderFragment childContent, string header = "")
    {
        Show(PopupType.Standard, new StandardPopupParams
        {
            HeaderText = header,
            ChildContent = childContent
        });
    }

}

public class ConfirmationPopupParams
{
    public string Message { get; set; } = "";
    public string ButtonText { get; set; } = "OK";
    public EventCallback<bool> OnConfirm { get; set; }
    public ConfirmationType Type { get; set; } = ConfirmationType.Primary;
}
public class InfoPopupParams
{
    public string Message { get; set; } = "";
    public string Info { get; set; }
    public EventCallback<bool> OnConfirm { get; set; }
    public ConfirmationType Type { get; set; } = ConfirmationType.Primary;
}
public enum ConfirmationType
{
    Primary,
    Danger
}

public class InvalidPopupParams
{
    public string Message { get; set; } = "";
}

public class NewPopupParams
{
    public string HeaderText { get; set; } = "";
    public RenderFragment? ChildContent { get; set; }
}

public class StandardPopupParams
{
    public string HeaderText { get; set; } = "";
    public RenderFragment? ChildContent { get; set; }
}


