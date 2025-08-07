using Microsoft.AspNetCore.Components.Web;

namespace TaskyRevamp.Client;

public class StaticClasses
{
    public static InteractiveWebAssemblyRenderMode InteractiveWebAssemblyRenderModeFalse { get; } = new(false);
    public static InteractiveAutoRenderMode InteractiveAutoRenderModeFalse { get; } = new(false);
    public static InteractiveServerRenderMode InteractiveServerRenderModeFalse { get; } = new(false);

}