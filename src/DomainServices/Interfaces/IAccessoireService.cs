
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IAccessoireService
    {
        Task<List<Accessoire>> GetAccessoires();
        Task<Accessoire> GetAccessoire(int Id);
        Task<Accessoire> CreateAccessoire(Accessoire beestje);

        Task EditAccessoire(int Id, Accessoire beestje);
        Task DeleteAccessoire(int Id);
    }
}
