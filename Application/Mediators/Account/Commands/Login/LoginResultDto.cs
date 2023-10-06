using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Application.Mediators.Account.Commands.Login;

public class LoginResultDto
{


    public string Access_token { get; set; }
    public int Expires_in { get; set; }
    public string Token_type { get; set; }
    public string Refresh_token { get; set; }
    public string Scope { get; set; }




}