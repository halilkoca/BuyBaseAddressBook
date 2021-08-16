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
                    UUID = "602d2149e773f2a3990b4711",
                    Name = "Halil",
                    LastName = "Koca",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4712",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320230"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4713",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "ihalilkoca@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4714",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "İstanbul"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4715",
                    Name = "Fatma",
                    LastName = "Acar",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4716",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320231"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4717",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "fatmaacar@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4718",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "İstanbul"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4719",
                     Name = "Özde",
                    LastName = "Acarkan",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4720",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320233"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4721",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "ozdeacarkan@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4722",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "Muğla"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4723",
                     Name = "Atahan",
                    LastName = "Adanır",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4724",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320234"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4725",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "atahanadanir@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4726",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "İzmir"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4727",
                     Name = "Hacı Mehmet",
                    LastName = "Adıgüzel",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4728",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320235"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4729",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "hacimehmetadiguzel@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4730",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "Ankara"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4731",
                    Name = "Mükerrem Zeynep",
                    LastName = "Ağca",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4732",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905327320236"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4733",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "mukerremzeynepagca@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4734",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "Ankara"
                        }
                    }
                }
            };
        }
    }
}
