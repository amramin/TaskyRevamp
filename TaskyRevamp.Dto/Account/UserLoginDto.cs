
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace TaskyRevamp.Dto.Account;

public class UserLoginDto
{
    //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]

    public string Username { get; set; }

    
    //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]

    public string Password { get; set; } 

     
}