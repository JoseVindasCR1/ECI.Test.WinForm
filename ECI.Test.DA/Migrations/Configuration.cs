namespace ECI.Test.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ECI.Test.Shared.Models;
    using ECI.Test.Shared.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<TestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DogWalker.DA.DogWalkerDbContext";
        }

        protected override void Seed(TestDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddOrUpdate(u => u.UserName,
                    new User 
                    { 
                        UserName = "admin", 
                        Password = PasswordHasher.HashPassword("admin123"), 
                        CreateDate = DateTime.Now.AddDays(-30) 
                    },
                    new User 
                    { 
                        UserName = "user1", 
                        Password = PasswordHasher.HashPassword("user123"), 
                        CreateDate = DateTime.Now.AddDays(-20) 
                    },
                    new User 
                    { 
                        UserName = "walker1", 
                        Password = PasswordHasher.HashPassword("walker123"), 
                        CreateDate = DateTime.Now.AddDays(-15) 
                    },
                    new User 
                    { 
                        UserName = "manager", 
                        Password = PasswordHasher.HashPassword("manager123"), 
                        CreateDate = DateTime.Now.AddDays(-10) 
                    }
                );
                context.SaveChanges();
            }

            if (!context.Clients.Any())
            {
                context.Clients.AddOrUpdate(c => c.Name,
                    new Client { Name = "John Smith", Phone = "555-0101" },
                    new Client { Name = "Jane Doe", Phone = "555-0102" },
                    new Client { Name = "Bob Wilson", Phone = "555-0103" },
                    new Client { Name = "Alice Brown", Phone = "555-0104" },
                    new Client { Name = "Charlie Davis", Phone = "555-0105" }
                );
                context.SaveChanges();
            }

            if (!context.Dogs.Any())
            {
                context.Dogs.AddOrUpdate(d => d.Name,
                    new Dog { Name = "Max", Breed = "Golden Retriever", Age = 3 },
                    new Dog { Name = "Bella", Breed = "Labrador", Age = 5 },
                    new Dog { Name = "Charlie", Breed = "Beagle", Age = 2 },
                    new Dog { Name = "Luna", Breed = "German Shepherd", Age = 4 },
                    new Dog { Name = "Cooper", Breed = "Poodle", Age = 1 },
                    new Dog { Name = "Daisy", Breed = "Bulldog", Age = 6 },
                    new Dog { Name = "Rocky", Breed = "Boxer", Age = 3 },
                    new Dog { Name = "Sadie", Breed = "Corgi", Age = 2 }
                );
                context.SaveChanges();
            }

            if (!context.ClientDogs.Any())
            {
                var johnSmith = context.Clients.FirstOrDefault(c => c.Name == "John Smith");
                var janeDoe = context.Clients.FirstOrDefault(c => c.Name == "Jane Doe");
                var bobWilson = context.Clients.FirstOrDefault(c => c.Name == "Bob Wilson");
                var aliceBrown = context.Clients.FirstOrDefault(c => c.Name == "Alice Brown");
                var charlieDavis = context.Clients.FirstOrDefault(c => c.Name == "Charlie Davis");

                var max = context.Dogs.FirstOrDefault(d => d.Name == "Max");
                var bella = context.Dogs.FirstOrDefault(d => d.Name == "Bella");
                var charlie = context.Dogs.FirstOrDefault(d => d.Name == "Charlie");
                var luna = context.Dogs.FirstOrDefault(d => d.Name == "Luna");
                var cooper = context.Dogs.FirstOrDefault(d => d.Name == "Cooper");
                var daisy = context.Dogs.FirstOrDefault(d => d.Name == "Daisy");
                var rocky = context.Dogs.FirstOrDefault(d => d.Name == "Rocky");
                var sadie = context.Dogs.FirstOrDefault(d => d.Name == "Sadie");

                if (johnSmith != null && max != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = johnSmith.Id, DogId = max.Id });
                if (johnSmith != null && bella != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = johnSmith.Id, DogId = bella.Id });
                if (janeDoe != null && charlie != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = janeDoe.Id, DogId = charlie.Id });
                if (janeDoe != null && luna != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = janeDoe.Id, DogId = luna.Id });
                if (bobWilson != null && cooper != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = bobWilson.Id, DogId = cooper.Id });
                if (aliceBrown != null && daisy != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = aliceBrown.Id, DogId = daisy.Id });
                if (aliceBrown != null && rocky != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = aliceBrown.Id, DogId = rocky.Id });
                if (charlieDavis != null && sadie != null)
                    context.ClientDogs.AddOrUpdate(cd => new { cd.ClientId, cd.DogId },
                        new ClientDog { ClientId = charlieDavis.Id, DogId = sadie.Id });

                context.SaveChanges();
            }

            if (!context.Walks.Any())
            {
                var johnSmith = context.Clients.FirstOrDefault(c => c.Name == "John Smith");
                var janeDoe = context.Clients.FirstOrDefault(c => c.Name == "Jane Doe");
                var bobWilson = context.Clients.FirstOrDefault(c => c.Name == "Bob Wilson");
                var aliceBrown = context.Clients.FirstOrDefault(c => c.Name == "Alice Brown");

                var max = context.Dogs.FirstOrDefault(d => d.Name == "Max");
                var bella = context.Dogs.FirstOrDefault(d => d.Name == "Bella");
                var charlie = context.Dogs.FirstOrDefault(d => d.Name == "Charlie");
                var cooper = context.Dogs.FirstOrDefault(d => d.Name == "Cooper");
                var daisy = context.Dogs.FirstOrDefault(d => d.Name == "Daisy");

                if (johnSmith != null && max != null)
                    context.Walks.Add(new Walk { ClientId = johnSmith.Id, DogId = max.Id, Date = DateTime.Now.AddDays(-5), Duration = 30 });
                if (johnSmith != null && bella != null)
                    context.Walks.Add(new Walk { ClientId = johnSmith.Id, DogId = bella.Id, Date = DateTime.Now.AddDays(-3), Duration = 45 });
                if (janeDoe != null && charlie != null)
                    context.Walks.Add(new Walk { ClientId = janeDoe.Id, DogId = charlie.Id, Date = DateTime.Now.AddDays(-2), Duration = 20 });
                if (bobWilson != null && cooper != null)
                    context.Walks.Add(new Walk { ClientId = bobWilson.Id, DogId = cooper.Id, Date = DateTime.Now.AddDays(-1), Duration = 60 });
                if (aliceBrown != null && daisy != null)
                    context.Walks.Add(new Walk { ClientId = aliceBrown.Id, DogId = daisy.Id, Date = DateTime.Now, Duration = 30 });

                context.SaveChanges();
            }
        }
    }
}
