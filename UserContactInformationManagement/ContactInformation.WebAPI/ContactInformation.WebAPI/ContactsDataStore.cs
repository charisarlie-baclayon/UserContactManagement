using System.Collections.Generic;
using ContactInformation.WebAPI.Models;

namespace ContactInformation.WebAPI
{
    public class ContactsDataStore
    {
        public List<Contact> Contacts { get; set; }
        public static ContactsDataStore Current { get; set; } = new ContactsDataStore();

        public ContactsDataStore()
        {
            Contacts = new List<Contact>()
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "Charis Arlie",
                    LastName = "Baclayon",
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Id = 1,
                            AddressDescription = "Guadalupe",
                            AddressType = AddressType.Delivery.ToString(),
                            ContactId = 1
                        },
                        new Address()
                        {
                            Id = 2,
                            AddressDescription = "Guadalupe",
                            AddressType = "Billing",
                            ContactId = 1
                        }
                    },
                    PhoneNumber = 1234567890,
                    EmailAddress = "charis.arlie@example.com",
                    BirthDate = new DateTime(1995, 5, 20),
                    Favorite = true,
                    UserId = 1
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Leonel Christie",
                    LastName = "Baclayon",
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Id = 3,
                            AddressDescription = "Guadalupe",
                            AddressType = "Delivery",
                            ContactId = 2
                        },
                        new Address()
                        {
                            Id = 4,
                            AddressDescription = "Guadalupe",
                            AddressType = "Billing",
                            ContactId = 2
                        }
                    },
                    PhoneNumber = 987654321,
                    EmailAddress = "leonel.christie@example.com",
                    BirthDate = new DateTime(1998, 8, 15),
                    Favorite = false,
                    UserId = 2
                },
                new Contact()
                {
                    Id = 3,
                    FirstName = "Grace Christian",
                    LastName = "Baclayon",
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            Id = 5,
                            AddressDescription = "Guadalupe",
                            AddressType = "Delivery",
                            ContactId = 3
                        },
                        new Address()
                        {
                            Id = 6,
                            AddressDescription = "Guadalupe",
                            AddressType = "Billing",
                            ContactId = 3
                        }
                    },
                    PhoneNumber = 555555555,
                    EmailAddress = "grace.christian@example.com",
                    BirthDate = new DateTime(2000, 10, 5),
                    Favorite = true,
                    UserId = 3
                },
            };
        }
    }
}
