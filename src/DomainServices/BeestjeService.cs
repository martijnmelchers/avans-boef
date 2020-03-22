using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Models;
using Models.Repository.Interfaces;
using Type = Models.Type;

namespace DomainServices
{
    public class BeestjeService : IBeestjeService
    {
        private readonly IBeestjeRepository _beestjeRepository;
        private readonly IAccessoireRepository _accessoireRepository;


        public BeestjeService(IBeestjeRepository beestjeRepository, IAccessoireRepository accessoireRepository)
        {
            _beestjeRepository = beestjeRepository;
            _accessoireRepository = accessoireRepository;
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
             
             currentBeestje.Accessoires = beestje.Accessoires;
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

        public async Task<List<(Beestje beestje, bool available)>> GetAvailableBeestjes(Booking booking)
        {
            var beestjes = await _beestjeRepository.GetAllWhere(b => b.BookingBeestjes.FirstOrDefault(bookings => bookings.Booking.Date == booking.Date && !bookings.Booking.Equals(booking)) == null);
            var availableBeestjes = beestjes.Select(beestje => (beestje, IsValid(booking.Date, beestje))).ToList();
            
            return availableBeestjes;
        }


        public async Task<Beestje> SelectAccessoires(Beestje beestje, List<int> accessoires)
        {
            foreach (var accessoireId in accessoires)
            {
                var accessoire = await _accessoireRepository.Get(accessoireId);
                if (beestje.Accessoires == null)
                {
                    beestje.Accessoires = new List<Accessoire>();
                }
                beestje.Accessoires.Add(accessoire);
            }

            return beestje;
        }


        private static bool IsValid(DateTime date, Beestje beestje)
        {
            if (beestje.Name == "Penguïn")
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    return false;

            return beestje.Type switch
            {
                Type.Sneeuw when date.Month >= 6 && date.Month <= 8 => false,
                Type.Woestijn when (date.Month >= 10 && date.Month <= 12) || (date.Month >= 1 && date.Month <= 2) => false,
                _ => true
            };
        }
    }
}
