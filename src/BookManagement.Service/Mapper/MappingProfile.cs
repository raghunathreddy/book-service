using AutoMapper;
using BookManagement.Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using BookManagement.Model;

namespace BookManagement.Service.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, DtoBook>().ReverseMap();
            CreateMap<BookExchange, DtoBookExchange>().ReverseMap();
            CreateMap<BookExchangeTX, DtoBookExchangeTX>().ReverseMap();
            //CreateMap<UserActivation, User>().ReverseMap();
            //CreateMap<UserMusicTrack, MusicTrack>().ReverseMap();
        }
    }
}
