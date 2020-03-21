using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IBeestjeService
    {
        Task<List<Beestje>> GetBeestjes();
        Task<Beestje> GetBeestje(int id);
        Task<Beestje> CreateBeestje(Beestje beestje);

        Task EditBeestje(int id, Beestje beestje);
        Task DeleteBeestje(int id);

        Task<List<Beestje>> GetAvailableBeestjesByDate(DateTime date);
    }
}
