using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions;

public static class ApiError
{
    public static string PrentNotFound = "PrentNotFound";
    public static string AddTaskSettingNotFound = "AddTaskSettingNotFound";
    public static string FilterFieldNotFound = "FilterFieldNotFound";
    public static string DefaultColumnNotFound = "DefaultColumnNotFound";
    public static string PrivilegeNotFound = "PrivilegeNotFound";
}
