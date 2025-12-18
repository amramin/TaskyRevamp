using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Infrastructure;

public class NetworkShareAccess : IDisposable
{
    private IntPtr userToken = IntPtr.Zero;
    private WindowsIdentity identity;
    private IDisposable impersonationContext;

    public NetworkShareAccess(string domain, string username, string password)
    {
        // LogonUser for impersonation
        if (!LogonUser(
            username,
            domain,
            password,
            (int)LogonType.LOGON32_LOGON_NEW_CREDENTIALS,
            (int)LogonProvider.LOGON32_PROVIDER_DEFAULT,
            out userToken))
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new UnauthorizedAccessException($"LogonUser failed with error code {errorCode}");
        }

        // Create a WindowsIdentity object from the impersonated token
        identity = new WindowsIdentity(userToken);
        impersonationContext = new WindowsImpersonationContext(identity);
    }

    public void Dispose()
    {
        impersonationContext?.Dispose();
        identity?.Dispose();

        if (userToken != IntPtr.Zero)
        {
            CloseHandle(userToken);
            userToken = IntPtr.Zero;
        }
    }

    // Windows API methods
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool LogonUser(
        string lpszUsername,
        string lpszDomain,
        string lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        out IntPtr phToken);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);

    private enum LogonType
    {
        LOGON32_LOGON_INTERACTIVE = 2,
        LOGON32_LOGON_NETWORK = 3,
        LOGON32_LOGON_BATCH = 4,
        LOGON32_LOGON_SERVICE = 5,
        LOGON32_LOGON_UNLOCK = 7,
        LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
        LOGON32_LOGON_NEW_CREDENTIALS = 9
    }

    private enum LogonProvider
    {
        LOGON32_PROVIDER_DEFAULT = 0
    }
}

// Helper class to manage impersonation context
public class WindowsImpersonationContext : IDisposable
{
    private readonly WindowsIdentity identity;
    public WindowsImpersonationContext(WindowsIdentity identity)
    {
        this.identity = identity ?? throw new ArgumentNullException(nameof(identity));
    }

    public void Dispose()
    {
        // This class is used to hold impersonation; no actual operation here in the latest .NET version.
        identity.Dispose();
    }
}
