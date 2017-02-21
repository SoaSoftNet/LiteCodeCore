﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using LiteCode.Entity;
using LiteCode.WebSite.Areas.SysManager;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Lucky.SiteManager.Controllers
{
    public class AccountController : BaseController
    {
        private ILogger _logger;
        private UserManager<SysUsers> _userManager;
        private SignInManager<SysUsers> _signInManager;
        public AccountController(ILogger<AccountController> logger , UserManager<SysUsers> userManager, SignInManager<SysUsers> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password, bool rememberme, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                SysUsers manager = await _userManager.FindByNameAsync(username);
                var result =await _userManager.CheckPasswordAsync(manager, password);
                switch (result)
                {
                    case true:
                        if (returnUrl != null)
                        {
                            //await _signInManager.SignInAsync(manager, true);
                            SetCalims(username, rememberme, manager.Id);
                            return Redirect("~" + returnUrl);
                        }
                        else
                        {
                            //await _signInManager.SignInAsync(manager, true);
                            SetCalims(username, rememberme, manager.Id);
                        }
                        return Redirect("/SysManager/Home/Index");
                    default:
                        return View();
                }
            }
            catch (Exception ex)
            {
                
                
            }

            return View();
        }

       
        public async Task<ActionResult> LoginOut()
        {
            //await _signInManager.SignOutAsync();
           await HttpContext.Authentication.SignOutAsync("SysManager");
            return RedirectToAction("Login");
        }
        private void SetCalims(string username, bool rememberme,string userid)
        {
            var identity = new ClaimsIdentity("SysManager");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));

            identity.AddClaim(new Claim(ClaimTypes.Role, "546695139276357632"));
            identity.AddClaim(new Claim("User_Id", userid));
            var principal = new ClaimsPrincipal(identity);
            
            HttpContext.Authentication.SignInAsync("SysManager", principal, new AuthenticationProperties { IsPersistent = true });
        }

        
    }
}