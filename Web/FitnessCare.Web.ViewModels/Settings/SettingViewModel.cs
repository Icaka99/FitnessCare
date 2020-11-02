namespace FitnessCare.Web.ViewModels.Settings
{
    using FitnessCare.Data.Models;
    using FitnessCare.Services.Mapping;

    using AutoMapper;

    public class SettingViewModel : IMapFrom<Setting>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameAndValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Setting, SettingViewModel>().ForMember(
                m => m.NameAndValue,
                opt => opt.MapFrom(x => x.Name + " = " + x.Value));
        }
    }
}
