using AutoMapper;
using BackEnd.Models;

namespace BackEnd.Mappers;

public static class DataBaseMapper
{
    private static IMapper _mapper;

    public static void InitialiseMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LoginInfoEntity, LoginInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            cfg.CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        });

        _mapper = config.CreateMapper();
    }

    public static IMapper GetMapper()
    {
        if (_mapper == null)
        {
            InitialiseMapper();
        }
        return _mapper;
    }
}