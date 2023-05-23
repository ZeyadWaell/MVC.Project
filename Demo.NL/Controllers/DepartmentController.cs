using Demo.DAL.Entites;
using Demo.PLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.NL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepostory _Department;
        public DepartmentController(IDepartmentRepostory Departments)
        {
            _Department = Departments;
        }
        public async Task<IActionResult> Index()
		{
			ViewData["Message"] = "Hello World from Controller";
			var departments = await _Department.GetAll();
			return View(departments);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Create(Department dep)
        {
			if(ModelState.IsValid)
			{
               await _Department.Add(dep);
				return RedirectToAction(nameof(Index));
            }
			
            return View(dep);
        }
		public async Task<IActionResult> Details(int? Id)
		{
			var FinThem = await _Department.Get(Id);// اساله ليه مش X=X.ID
			if(FinThem == null)
			{
				return NotFound();
			}
			return View(FinThem);
		}
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            var FindThem = await _Department.Get(Id); 
			if(FindThem == null) { 
				return NotFound(); 
			}
            return View(FindThem);
        }
        [HttpPost]
		public async Task<IActionResult> Edit(Department department)
		{
			if(ModelState.IsValid)
			{
                await _Department.Update(department);
		        return RedirectToAction(nameof(Index));
			}
			return View(department);
		}
		public  async Task<IActionResult> Delete(int? Id)
		{
			var FindThem = await _Department.Get(Id);
			if(ModelState.IsValid)
			{
			    await _Department.Delete(FindThem);
				return RedirectToAction(nameof(Index));
			}
            return RedirectToAction(nameof(Index));
        }
    }
}
