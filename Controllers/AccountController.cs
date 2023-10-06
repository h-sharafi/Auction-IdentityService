using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Application.Mediators.Account.Commands.Login;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

public class AccountController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginCommand loginCommand) =>
            Ok(await Mediator.Send(loginCommand));
}