using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;
using Type = Models.Type;

namespace UnitTests.Helpers
{
    public class ModelMocks
    {
        public List<Beestje> Beestjes { get; set; }
        public List<Models.Accessoire> Accessoires { get; set; }
        public List<Models.Booking> Bookings { get; set; }
        public ModelMocks()
        {
           
            Accessoires = new List<Models.Accessoire>()
            {
                new  Models.Accessoire() { Id = 1, Image = "ijsbeer.png", Name = "Banaan", Price = 3 },
                new  Models.Accessoire() { Id = 2, Image = "ijsbeer.png", Name = "Zadel", Price = 50 },
                new  Models.Accessoire() { Id = 3, Image = "ijsbeer.png", Name = "Krukje", Price = 60 },
                new  Models.Accessoire() { Id = 4, Image = "ijsbeer.png", Name = "Zweep", Price = 20 },
                new  Models.Accessoire() { Id = 5, Image = "ijsbeer.png", Name = "Bal", Price = 10 },
                new  Models.Accessoire() { Id = 6, Image = "ijsbeer.png", Name = "Dansschoenen", Price = 200 },
            };

            Beestjes = new List<Beestje>()
            {
                new Beestje()
                {
                    Id = 1, Name = "Aap", Type = Type.Jungle, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 1, BeestjeId = 1 }
                        }
                },
                new Beestje()
                {
                    Id = 2, Name = "Olifant", Type = Type.Jungle, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 3, Name = "Zebra", Type = Type.Jungle, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 2, BeestjeId = 3 }
                        }
                },
                new Beestje()
                {
                    Id = 4, Name = "Leeuw", Type = Type.Jungle, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 3, BeestjeId = 4 },
                            new BeestjeAccessoires() { AccessoireId = 4, BeestjeId = 4 }
                        }
                },
                new Beestje()
                {
                    Id = 5, Name = "Hond", Type = Type.Boerderij, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 5, BeestjeId = 5 }
                        }
                },
                new Beestje()
                {
                    Id = 6, Name = "Ezel", Type = Type.Boerderij, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 7, Name = "Koe", Type = Type.Boerderij, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 8, Name = "Eend", Type = Type.Boerderij, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 9, Name = "Kuiken", Type = Type.Boerderij, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 10, Name = "Penguïn", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 6, BeestjeId = 10 }
                        }
                },
                new Beestje()
                {
                    Id = 11, Name = "Ijsbeer", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 12, Name = "Zeehond", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                            new BeestjeAccessoires() { AccessoireId = 5, BeestjeId = 12 }
                        }
                },
                new Beestje()
                {
                    Id = 13, Name = "Kameel", Type = Type.Woestijn, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 14, Name = "Slang", Type = Type.Woestijn, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 15, Name = "abcdefghijklmnopqrstuvwxyz", Type = Type.Woestijn, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },

                new Beestje()
                {
                    Id = 16, Name = "Eend", Type = Type.Boerderij, Price = 220, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },

                new Beestje()
                {
                    Id = 17, Name = "Penguin 1", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 18, Name = "Penguin 2", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
                new Beestje()
                {
                    Id = 19, Name = "Penguin 3", Type = Type.Sneeuw, Price = 200, BeestjeAccessoires =
                        new List<BeestjeAccessoires>()
                        {
                        }
                },
            };


            Bookings = new List<Models.Booking>()
            {
                new Models.Booking()
                {
                    Id = 1,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 1, BookingId = 1 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires() { BookingId = 1, AccessoireId = 1 }
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },
                new Models.Booking()
                {
                    Id = 2,
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 2, BookingId = 2 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires() { BookingId = 2, AccessoireId = 1 }
                    },
                    InitialPrice = 203,
                    Step = BookingStep.Price,
                },
                new Models.Booking()
                {
                    Id = 3,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 10, BookingId = 3 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires() { BookingId = 3, AccessoireId = 6 }
                    },
                    Step = BookingStep.Beestjes
                },

                new Models.Booking()
                {
                    Id = 4,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 1, BookingId = 4 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires() { BookingId = 4, AccessoireId = 1 }
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },

                new Models.Booking() // Used for testing day discount
                {
                    Id = 5,
                    Date =  new DateTime(2020,03,23),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 2, BookingId = 5 }
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },

                new Models.Booking() // Used for testing max discount
                {
                    Id = 6,
                    Date =  new DateTime(2020,03,23),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 15, BookingId = 6 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },

                new Models.Booking() // Used for testing name discount.
                {
                    Id = 7,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 1, BookingId = 7 }
                    },
                    BookingAccessoires = new List<BookingAccessoires>()
                    {
                        new BookingAccessoires() { BookingId = 7, AccessoireId = 1 }
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Price,
                },

                new Models.Booking() // Used for testing duck discount.
                {
                    Id = 8,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 16, BookingId = 8 }
                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },

                new Models.Booking() // Used for testing Type discount.
                {
                    Id = 9,
                    Date =  new DateTime(2020,03,22),
                    BookingBeestjes = new List<BookingBeestje>()
                    {
                        new BookingBeestje() { BeestjeId = 17, BookingId = 9 },
                        new BookingBeestje() { BeestjeId = 18, BookingId = 9 },
                        new BookingBeestje() { BeestjeId = 19, BookingId = 9 }


                    },
                    InitialPrice = 203,
                    FinalPrice = 198.94m,
                    Step = BookingStep.Finished,
                },
            };


        }

        public ApplicationDbContext InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BOEF")
                .Options;

            var context = new ApplicationDbContext(options);
            
            context.Accessoires.AddRange(Accessoires);
            context.Beestjes.AddRange(Beestjes);
            context.Bookings.AddRange(Bookings);

            context.SaveChanges();

            return context;
        }
    }
}
