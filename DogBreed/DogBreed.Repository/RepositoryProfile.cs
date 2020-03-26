using AutoMapper;
using DogBreed.DAL.Entities;
using DogBreed.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.Repository
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            CreateMap<UserEntity, IUser>().ReverseMap();
            CreateMap<DogImageEntity, IDogImage>().ReverseMap();
            CreateMap<PredictionResultEntity, IPredictionResult>().ReverseMap();
        }
    }
}
