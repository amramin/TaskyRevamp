
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Account;

public class UserLoginDto
{
    [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]

    public string Username { get; set; }


    [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]

    public string Password { get; set; }


}