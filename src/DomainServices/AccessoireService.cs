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
    public class AccessoireService : IAccessoireService
    {
        private readonly IAccessoireRepository _accessoireRepository;

        public AccessoireService(IAccessoireRepository AccessoireRepository)
        {
            _accessoireRepository = AccessoireRepository;
        }

        public async Task<Accessoire> CreateAccessoire(Accessoire Accessoire)
        {
            return await _accessoireRepository.Insert(Accessoire);
        }

        public async Task DeleteAccessoire(int Id)
        {
            await _accessoireRepository.Delete(Id);
            return;
        }

        public async Task EditAccessoire(int Id, Accessoire Accessoire)
        {
             var currentAccessoire = await _accessoireRepository.Get(Id);
             currentAccessoire.Id         = Accessoire.Id;
             currentAccessoire.Image      = Accessoire.Image;
             currentAccessoire.Name       = Accessoire.Name;
             currentAccessoire.Price      = Accessoire.Price;

             return;
        }

        public async  Task<Accessoire> GetAccessoire(int Id)
        {
            return await _accessoireRepository.GetWhere(Accessoire => Accessoire.Id == Id);
        }

        public async Task<List<Accessoire>> GetAccessoires()
        {
            return await _accessoireRepository.GetAll();
        }
    }
}
