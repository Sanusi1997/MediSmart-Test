using AutoMapper;
using MediSmart.API.Data.DTO;
using MediSmart.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Data
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() {

            CreateMap<Record, RecordDTO>().ReverseMap();
            CreateMap<RecordCreateDTO, Record>().ReverseMap();
            CreateMap<RecordUpdateDTO, Record>().ReverseMap();
        
        }
    }
}
