using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.NL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task<IActionResult> Index(string SerachVlaue)
        {
            if (string.IsNullOrEmpty(SerachVlaue))
            {
                var user = _userManager.Users.ToList();
                return View(user);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(SerachVlaue);
                return View(new List<ApplicationUser> { user });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user  = await _userManager.FindByIdAsync(id);
            if(id is null) 
                return NotFound();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string Id, ApplicationUser applicationUser)
        {
            if (Id != applicationUser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id);

                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();
                    user.PhoneNumber = applicationUser.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);


                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return View(applicationUser);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            if(Id==null)
                return NotFound(ModelState);
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            catch(System.Exception) { 
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
