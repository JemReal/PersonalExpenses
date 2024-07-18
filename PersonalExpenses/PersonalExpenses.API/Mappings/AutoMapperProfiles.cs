using AutoMapper;
using PersonalExpenses.API.Models.Domain;
using PersonalExpenses.API.Models.DTO;

namespace PersonalExpenses.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Mapping source and destination with different field names.
            //CreateMap<UserDTO, UserDomain>()
            //{
            //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
            //    .ReverseMap();
            //}
            #endregion

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();
            CreateMap<AddExpenseRequestDto, Expense>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Frequency, FrequencyDto>().ReverseMap();
            CreateMap<UpdateExpenseRequestDto, Expense>().ReverseMap();
        }
    }

    #region Mapping source and destination with different field names.
    //public class UserDTO
    //{
    //    public string FullName { get; set; }
    //}

    //public class UserDomain
    //{
    //    public string Name { get; set; }
    //}
    #endregion
}
