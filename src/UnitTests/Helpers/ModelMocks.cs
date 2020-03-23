using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;
using Type = Models.Type;

namespace UnitTests.Helpers
{
    class ModelMocks
    {
        public List<Beestje> Beestjes { get; set; }
        public List<Accessoire> Accessoires { get; set; }
        public List<Booking> Bookings { get; set; }

        public ApplicationDbContext Context { get; set; }

        public ModelMocks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BOEF")
                .Options;

            Context = new ApplicationDbContext(options);

            Accessoires = new List<Accessoire>()
            {
                new Accessoire(){Id= 1, Image = "ijsbeer.png" ,Name = "Banaan", Price = 3},
                new Accessoire(){Id= 2, Image = "ijsbeer.png" ,Name = "Zadel", Price = 50},
                new Accessoire(){Id= 3, Image = "ijsbeer.png" ,Name = "Krukje", Price = 60},
                new Accessoire(){Id= 4, Image = "ijsbeer.png" ,Name = "Zweep", Price = 20},
                new Accessoire(){Id= 5, Image = "ijsbeer.png" ,Name = "Bal", Price = 10},
                new Accessoire(){Id= 6, Image = "ijsbeer.png" ,Name = "Dansschoenen", Price = 200},
            };
            Context.Accessoires.AddRange(Accessoires);

            Beestjes = new List<Beestje>()
            {
                new Beestje(){Id = 1, Name = "Aap", Type = Type.Jungle, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  1, BeestjeId = 1}
                }},
                new Beestje(){Id = 2, Name = "Olifant", Type = Type.Jungle, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 3, Name = "Zebra", Type = Type.Jungle, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  2, BeestjeId = 3}
                }},
                new Beestje(){Id = 4, Name = "Leeuw", Type = Type.Jungle, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  3, BeestjeId = 4},
                    new BeestjeAccessoires(){AccessoireId =  4, BeestjeId = 4}
                }},
                new Beestje(){Id = 5, Name = "Hond", Type = Type.Boerderij, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  5, BeestjeId = 5}
                }},
                new Beestje(){Id = 6, Name = "Ezel", Type = Type.Boerderij, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 7, Name = "Koe", Type = Type.Boerderij, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 8, Name = "Eend", Type = Type.Boerderij, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 9, Name = "Kuiken", Type = Type.Boerderij, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 10, Name = "Pinquïn", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  6, BeestjeId = 10}
                }},
                new Beestje(){Id = 11, Name = "Ijsbeer", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 12, Name = "Zeehond", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                    new BeestjeAccessoires(){AccessoireId =  5, BeestjeId = 12}
                }},
                new Beestje(){Id = 13, Name = "Kameel", Type = Type.Woestijn, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
                new Beestje(){Id = 14, Name = "Slang", Type = Type.Woestijn, Price = 200, BeestjeAccessoires = new List<BeestjeAccessoires>()
                {
                }},
            };
            Context.Beestjes.AddRange(Beestjes);


            Bookings = new List<Booking>()
            {
                new Booking()
                {
                    Id  = 1,
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje(){BeestjeId = 1, BookingId = 1}
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires(){BookingId = 1, AccessoireId = 1}
                    },
                    Discounts = new List<Discount>()
                    {
                        new Discount("Naam met de letter: a", 2),
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },
                new Booking()
                {
                    Id  = 2,
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje(){BeestjeId = 2, BookingId = 2}
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires(){BookingId = 2, AccessoireId = 1}
                    },
                    InitialPrice = 203,
                    Step = BookingStep.Price,
                },
            };

            Context.Bookings.AddRange(Bookings);


            Context.SaveChanges();
        }

    }
}
