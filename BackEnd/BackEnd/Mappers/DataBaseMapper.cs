using AutoMapper;
using BackEnd.Entities;
using BackEnd.Models;

namespace BackEnd.Mappers;

public static class DataBaseMapper
{
    private static IMapper? _mapper;

    public static IMapper? GetMapper()
    {
        if (_mapper == null)
        {
            InitializeMapper();
        }
        return _mapper;
    }
    
    private static void InitializeMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LoginInfoEntity, LoginInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            cfg.CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            cfg.CreateMap<UrlInfoEntity, UrlInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        });

        _mapper = config.CreateMapper();
    }
}