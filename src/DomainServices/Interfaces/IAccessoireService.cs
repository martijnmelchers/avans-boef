
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IAccessoireService
    {
        Task<List<Accessoire>> GetAccessoires();
        Task<Accessoire> GetAccessoire(int id);
        Task<Accessoire> CreateAccessoire(Accessoire beestje);

        Task EditAccessoire(int id, Accessoire beestje);
        Task DeleteAccessoire(int id);
    }
}
