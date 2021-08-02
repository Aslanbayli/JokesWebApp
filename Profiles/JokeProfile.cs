using AutoMapper;
using JokesWebApp.Models;
using JokesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Profiles
{
    public class JokeProfile : Profile
    {
        public JokeProfile()
        {
            CreateMap<Joke, JokeDetailsViewModel>();
            CreateMap<Joke, JokeCreateViewModel>();
            CreateMap<Joke, JokeEditViewModel>();
        }
    }
}
