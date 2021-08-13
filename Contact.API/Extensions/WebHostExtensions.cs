using Contact.API.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Contact.API.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));

            var contacts = database.GetCollection<ContactEntity>(config.GetValue<string>("DatabaseSettings:CollectionName"));

            bool existProduct = contacts.Find(p => true).Any();
            if (!existProduct)
                contacts.InsertManyAsync(GetPreconfiguredProducts());
            return host;
        }

        private static IEnumerable<ContactEntity> GetPreconfiguredProducts()
        {
            return new List<ContactEntity>()
            {
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47f5",
                    Name = "Halil",
                    LastName = "Koca",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "ihalilkoca@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "İstanbul"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47f6",
                    Name = "Fatma",
                    LastName = "Acar",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "fatmaacar@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "İstanbul"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47f7",
                     Name = "Özde",
                    LastName = "Acarkan",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "ozdeacarkan@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "Muğla"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47f8",
                     Name = "Atahan",
                    LastName = "Adanır",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "atahanadanir@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "İzmir"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47f9",
                     Name = "Hacı Mehmet",
                    LastName = "Adıgüzel",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "hacimehmetadiguzel@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "Ankara"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b47fa",
                     Name = "Mükerrem Zeynep",
                    LastName = "Ağca",
                    Firm = "SST",
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.EmailAddress,
                            Value = "mukerremzeynepagca@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            Type = InformationType.Location,
                            Value = "Ankara"
                        }
                    }
                }
            };
        }
    }
}
