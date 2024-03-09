using Assignment.Core.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, CountryDTO>()
                .ForMember(dest => dest.Borders, opt => opt.Ignore());

            CreateMap<IEnumerable<Country>, IEnumerable<CountryDTO>>()
                .ConvertUsing((src, dest, context) =>
                {
                    return src.Select(country =>
                    {
                        var countryDTO = context.Mapper.Map<CountryDTO>(country);
                        countryDTO.Borders = country.Borders
                            .Select(borderId => src.First(s => s.Id == borderId).CommonName)
                            .ToArray();
                        return countryDTO;
                    });
                });
        }
    }
}
