using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductCatalog.Models;

namespace ProductCatalog
{
    public class MappingProfile: Profile
    {
		public MappingProfile()
		{
			CreateMap<AddProductViewModel, Product>().ForMember(m => m.LastUpdated, opt => opt.UseValue(DateTime.Now));
			CreateMap<UpdateProductViewModel, Product>().ForMember(m => m.LastUpdated, opt => opt.UseValue(DateTime.Now));
		}
    }
}
