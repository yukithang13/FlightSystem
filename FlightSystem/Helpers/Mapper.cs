using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Model;

namespace FlightSystem.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Flight, FlightModel>().ReverseMap();
            CreateMap<DocumentInfo, DocumentModel>().ReverseMap();
            CreateMap<Group, GroupModel>().ReverseMap();
            CreateMap<GroupRole, GroupRoleModel>().ReverseMap();
            CreateMap<GroupInfo, GroupInfoModel>().ReverseMap();



        }
    }
}
