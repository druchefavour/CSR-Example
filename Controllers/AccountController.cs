using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.Sdk;

namespace Ibis_CSR_Tool.Controllers
{
    public class AccountController : Controller
    {
            private readonly IOktaClient _oktaClient;
            public AccountController(IOktaClient oktaClient = null)
            {
                _oktaClient = oktaClient;
            }
            public IActionResult Login()
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
                }

                return RedirectToAction("Index", "Home");
            }
        }
}