using AutoMapper;
using Demo.DAL.Entites;
using Demo.NL.Helper;
using Demo.NL.Models;
using Demo.PLL.Interfaces;
using Demo.PLL.Repostoies;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.NL.Controllers
{
	public class EmplyeeController : Controller
	{
		//private readonly IEmplyeeRepostory _Emplyee;
		//private readonly IDepartmentRepostory _Department;
		private readonly IUniteofWork _uniteofwork;
        private readonly IMapper _mapper;
        public EmplyeeController(IUniteofWork uniteofwork/*IEmplyeeRepostory Emplyee, IDepartmentRepostory Department,*/ ,IMapper mapper)
        {
            _uniteofwork = uniteofwork;
            //_Department = Department;
            //         _Emplyee = Emplyee;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SerachVlaue = "")
		{
			IEnumerable<Employe> emplyess;
            if (string.IsNullOrEmpty(SerachVlaue))
            {
                emplyess = await _uniteofwork.EmplyeeRepostory.GetAll();

			}
			else
			{
				emplyess = await _uniteofwork.EmplyeeRepostory.Search(SerachVlaue);
			
			}
            var mappedEmplyeds = _mapper.Map<IEnumerable<EmployeModelView>>(emplyess);
			return View(mappedEmplyeds);
        }
        [HttpGet]
		public async Task<IActionResult> Create()
		{
            TempData["success"] = " Created";
            var FindDep = await _uniteofwork.EmplyeeRepostory.GetAll();
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Create(EmployeModelView EmplyeViewModel)
        {
			if(ModelState.IsValid)
			{
				EmplyeViewModel.ImageUrl = DouctumersSettings.UploadFile(EmplyeViewModel.Imafe, "Img");
				var mapppedEmplyee = _mapper.Map<Employe>(EmplyeViewModel);
				await _uniteofwork.EmplyeeRepostory.Add(mapppedEmplyee);
				TempData["success"] = " Created";
				return RedirectToAction("Index");
            }
			
            return View(EmplyeViewModel);
        }
		public async Task<IActionResult> Details(int? Id)
		{
			var Department = await _uniteofwork.EmplyeeRepostory.GetDepartmentByEmplyeeId(Id);
			var Emplyee = await _uniteofwork.EmplyeeRepostory.Get(Id);// اساله ليه مش X=X.ID
            var mapppedEmplyee = _mapper.Map<EmployeModelView>(Emplyee);

            if (Emplyee == null)
			{
				return NotFound();
			}
			return View(mapppedEmplyee);
		}
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            var FindThem = await _uniteofwork.EmplyeeRepostory.Get(Id);
            var mapppedEmplyee = _mapper.Map<EmployeModelView>(FindThem);

            if (FindThem == null) { 
				return NotFound(); 
			}
            return View(mapppedEmplyee);
        }
        [HttpPost]
		public async Task<IActionResult> Edit(EmployeModelView Emplyee)
		{
            var mapppedEmplyee = _mapper.Map<Employe>(Emplyee);

            if (ModelState.IsValid)
			{
              
                await _uniteofwork.EmplyeeRepostory.Update(mapppedEmplyee);
		        return RedirectToAction(nameof(Index));
			}
			return View(mapppedEmplyee);
		}
		public  async Task<IActionResult> Delete(int? Id)
		{
			var FindThem = await _uniteofwork.EmplyeeRepostory.Get(Id);
			if(ModelState.IsValid)
			{
			    await _uniteofwork.EmplyeeRepostory.Delete(FindThem);
				return RedirectToAction(nameof(Index));
			}
            return RedirectToAction(nameof(Index));
        }
    }
}
