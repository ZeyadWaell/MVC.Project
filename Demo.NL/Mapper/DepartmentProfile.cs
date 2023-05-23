using AutoMapper;
using Demo.DAL.Entites;
using Demo.NL.Models;

namespace Demo.NL.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
