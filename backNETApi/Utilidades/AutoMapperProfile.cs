using AutoMapper;
using backNETApi.DTOs;
using backNETApi.Models;
using System.Globalization;

namespace backNETApi.Utilidades
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            #region Department

            CreateMap<Department,DepartmentDTO>().ReverseMap();

            #endregion

            #region Employee

            CreateMap<Employee,employeeDTO>().ForMember(
                destination=>destination.DepartmentName,
                opt=>opt.MapFrom(origin => origin.IdDepartmentNavigation.Name)
                ).ForMember(destination=>destination.ContractDate,
                opt=>opt.MapFrom(origin => origin.ContractDate.ToString("dd/MM/yyyy")));
            
            
            
            CreateMap<employeeDTO, Employee>().ForMember(
                destination=> destination.IdDepartmentNavigation,
                opt => opt.Ignore()
                ).ForMember(
                destination=>destination.ContractDate,
                opt=>opt.MapFrom(origin => DateTime.ParseExact(origin.ContractDate,"dd/MM/yyyy",CultureInfo.InvariantCulture))
                );

            #endregion
        }
    }
}
