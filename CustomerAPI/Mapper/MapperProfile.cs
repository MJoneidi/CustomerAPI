using AutoMapper;
using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {            
            CreateMap<CustomerRequest,Customer>();           
        }
    }
}
