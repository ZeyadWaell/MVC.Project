using AutoMapper;
using Demo.DAL.Entites;
using Demo.NL.Models;

namespace Demo.NL.Mapper
{
    public class EmplyeeProfile : Profile
    {
        public EmplyeeProfile()
        {
            CreateMap<Employe, EmployeModelView>().ReverseMap();
        }
    }
}
