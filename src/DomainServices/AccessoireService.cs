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

        public AccessoireService(IAccessoireRepository accessoireRepository)
        {
            _accessoireRepository = accessoireRepository;
        }

        public async Task<Accessoire> CreateAccessoire(Accessoire accessoire)
        {
            return await _accessoireRepository.Insert(accessoire);
        }

        public async Task DeleteAccessoire(int id)
        {
            await _accessoireRepository.Delete(id);
            return;
        }

        public async Task EditAccessoire(int id, Accessoire accessoire)
        {
             var currentAccessoire = await _accessoireRepository.Get(id);
             
             currentAccessoire.Id         = accessoire.Id;
             currentAccessoire.Image      = accessoire.Image;
             currentAccessoire.Name       = accessoire.Name;
             currentAccessoire.Price      = accessoire.Price;
        }

        public async  Task<Accessoire> GetAccessoire(int id)
        {
            return await _accessoireRepository.Get(id);
        }

        public async Task<List<Accessoire>> GetAccessoires()
        {
            return await _accessoireRepository.GetAll();
        }
    }
}
