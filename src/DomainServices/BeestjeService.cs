using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Models;
using Models.Exceptions;
using Models.Repository;
using Models.Repository.Interfaces;

namespace DomainServices
{
    public class BeestjeService : IBeestjeService
    {
        private readonly IBeestjeRepository _beestjeRepository;

        public BeestjeService(IBeestjeRepository beestjeRepository)
        {
            _beestjeRepository = beestjeRepository;
        }

        public async Task<Beestje> CreateBeestje(Beestje beestje)
        {
            return await _beestjeRepository.Insert(beestje);
        }

        public async Task DeleteBeestje(int Id)
        {
            await _beestjeRepository.Delete(Id);
        }

        public async Task EditBeestje(int id, Beestje beestje)
        {
             var currentBeestje = await _beestjeRepository.Get(id);
             
             currentBeestje.Accesoires = beestje.Accesoires;
             currentBeestje.Id         = beestje.Id;
             currentBeestje.Image      = beestje.Image;
             currentBeestje.Name       = beestje.Name;
             currentBeestje.Price      = beestje.Price;
             currentBeestje.Type       = beestje.Type;
        }

        public async  Task<Beestje> GetBeestje(int id)
        {
            return await _beestjeRepository.Get(id);
        }

        public async Task<List<Beestje>> GetBeestjes()
        {
            return await _beestjeRepository.GetAll();
        }

        public async Task<List<Beestje>> GetAvailableBeestjesByDate(DateTime date)
        {
            //TODO: Sascha dit maken
            return new List<Beestje>();
        }
    }
}
