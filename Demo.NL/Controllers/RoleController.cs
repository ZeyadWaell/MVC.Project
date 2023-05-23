using Demo.DAL.Entites;
using Demo.NL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.NL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanger;
        private readonly UserManager<ApplicationUser> _UserManager;
        public RoleController(RoleManager<IdentityRole> rolemanger,UserManager<ApplicationUser> UserManager)
        {
            _rolemanger = rolemanger;
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {
            var role = _rolemanger.Roles.ToList();
            return View(role);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if(ModelState.IsValid)
            {
                var result = await _rolemanger.CreateAsync(identityRole);
                if(result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View(identityRole);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            if(Id is null)
                return NotFound();

            var user = await _rolemanger.FindByIdAsync(Id);
            if (user == null)
                return NotFound();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult>Update(string Id,IdentityRole identityRole)
        {
            if(Id is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _rolemanger.FindByIdAsync(Id);
                    role.Name = identityRole.Name;
                    role.NormalizedName = identityRole.Name.ToUpper();

                    var result = await _rolemanger.UpdateAsync(role);
                    if(result.Succeeded)
                        return RedirectToAction("Index");
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return View(identityRole);
        }
        [HttpGet]
        public async Task<IActionResult> AdddOrRemoveUsersToRole(string Roleid)
        {
            ViewBag.RoleId = Roleid;

            var role = await _rolemanger.FindByIdAsync(Roleid);
            if (role == null)
                return NotFound();

           

            var users = new List<UserRoleViewModel>();

            foreach (var user in _UserManager.Users)
            {
                var userInRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };
                if(await _UserManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                users.Add(userInRole);
                
            }
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> AdddOrRemoveUsersToRole(List<UserRoleViewModel> models, string RoleId)
        {
            var role = await _rolemanger.FindByIdAsync(RoleId);

            if(role == null)
                return NotFound();

            if(ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await _UserManager.FindByIdAsync(item.UserId);
                    if (user == null)
                    {
                        if(item.IsSelected && !(await _UserManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _UserManager.AddToRoleAsync(user,role.Name);
                            
                        }else if(!item.IsSelected && await _UserManager.IsInRoleAsync(user, role.Name))
                        {
                            await _UserManager.RemoveFromRoleAsync(user,role.Name);
                        }
                    }
                }
                return RedirectToAction("Update",new {id=RoleId});
            }
            return View(models);
        }


    }
}
