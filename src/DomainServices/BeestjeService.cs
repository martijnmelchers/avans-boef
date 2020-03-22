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
            var beestjes = await _beestjeRepository.GetAllWhere(b => b.BookingBeestjes.FirstOrDefault(bookings => bookings.Booking.Date == booking.Date) == null);

            List<(Beestje beestje, bool available)> availableBeestjes = new List<(Beestje beestje, bool available)>();

            beestjes.ForEach((beestje) =>
            {
                availableBeestjes.Add((beestje, isValid(booking, beestje)));
            });
            return availableBeestjes;
        }


        private bool isValid(Booking booking, Beestje beestje)
        {
            var date = booking.Date;

            if (beestje.Name == "Penguïn")
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    return false;

            if (beestje.Type == Type.Sneeuw)
                if (date.Month >= 6 && date.Month <= 8)
                    return false;

            if (beestje.Type == Type.Woestijn)
                if ((date.Month >= 10 && date.Month <= 12) || (date.Month >= 1 && date.Month <= 2))
                    return false;

            return true;
        }
    }
}
