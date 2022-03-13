using AutoMapper;

namespace OnlineShoping.Application.Automapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
