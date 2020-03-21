using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IBeestjeService
    {
        Task<List<Beestje>> GetBeestjes();
        Task<Beestje> GetBeestje(int Id);
        Task<Beestje> CreateBeestje(Beestje beestje);

        Task EditBeestje(int Id, Beestje beestje);
        Task DeleteBeestje(int Id);
    }
}
