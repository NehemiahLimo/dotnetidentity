using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static webapp_security.Pages.IndexModel;

namespace webapp_security.Pages.Account
{
	public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credentials { get; set; }
        public void OnGet()
        {
            //this.Credentials = new Credential { UserName = "NehemiahLimo" };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            
                if(Credentials.UserName=="admin" && Credentials.Password == "admin")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim(ClaimTypes.Email, "admin@codesofff.ccom"),
                        new Claim("Department","HR"),
                        new Claim("Admin","true"),
                        new Claim("Manager","true"),
                        new Claim("EmploymentDate","2023-01-01")

                    };
                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credentials.RememberMe
                };

                    await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                    return RedirectToPage("/Index");
                }
            //create context
            return Page();


        }

        public class Credential
        {
            [Required]
            [Display(Name ="User Name")]
            public string UserName { get; set; }
            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name ="Remember me")]
            public bool RememberMe { get; set; }
        }
    }
}
