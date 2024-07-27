using AutoMapper;
using BusinessLayer.Model.Models;
using System;
using System.Globalization;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<CompanyDto, CompanyInfo>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(e => e.OccupationName, m => m.MapFrom(ei => ei.Occupation))
                .ForMember(e => e.PhoneNumber, m => m.MapFrom(ei => ei.Phone))
                .ForMember(e => e.LastModifiedDateTime, m => m.MapFrom(ei => ei.LastModified.ToString("F", DateTimeFormatInfo.InvariantInfo)));
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(e => e.Occupation, m => m.MapFrom(ei => ei.OccupationName))
                .ForMember(e => e.Phone, m => m.MapFrom(ei => ei.PhoneNumber))
                .ForMember(e => e.LastModified, m =>  m.MapFrom(ei => DateTime.Now));
        }
    }
}