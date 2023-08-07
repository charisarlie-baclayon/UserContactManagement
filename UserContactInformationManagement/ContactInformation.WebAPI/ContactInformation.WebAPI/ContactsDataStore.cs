using ContactInformation.WebAPI.Models;
using System;
using System.Collections.Generic;

namespace ContactInformation.WebAPI
{
    public class ContactsDataStore
    {
        public List<Contact> Contacts { get; set; }
        public List<User> Users { get; set; }
        public static ContactsDataStore Current { get; } = new ContactsDataStore();

        public ContactsDataStore()
        {
            Users = new List<User>()
            {
                new User()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    PasswordHash = new byte[0], // Replace with the actual password hash
                    PasswordSalt = new byte[0], // Replace with the actual password salt
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            FirstName = "Charis Arlie",
                            LastName = "Baclayon",
                            Addresses = new List<Address>()
                            {
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Delivery",
                                },
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Work"
                                }
                            },
                            PhoneNumber = "1234567890",
                            EmailAddress = "charis.arlie@example.com",
                            BirthDate = new DateTime(1995, 5, 20),
                            Favorite = true,
                        }
                    }
                },
                new User()
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Username = "janesmith",
                    PasswordHash = new byte[0], // Replace with the actual password hash
                    PasswordSalt = new byte[0], // Replace with the actual password salt
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            FirstName = "Leonel Christie",
                            LastName = "Baclayon",
                            Addresses = new List<Address>()
                            {
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Delivery"
                                },
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Billing"
                                }
                            },
                            PhoneNumber = "987654321",
                            EmailAddress = "leonel.christie@example.com",
                            BirthDate = new DateTime(1998, 8, 15),
                            Favorite = false,
                        },
                        new Contact()
                        {
                            FirstName = "Grace Christian",
                            LastName = "Baclayon",
                            Addresses = new List<Address>()
                            {
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Delivery"
                                },
                                new Address()
                                {
                                    AddressDescription = "Guadalupe",
                                    AddressType = "Billing"
                                }
                            },
                            PhoneNumber = "5555555555",
                            EmailAddress = "grace.christian@example.com",
                            BirthDate = new DateTime(2000, 10, 5),
                            Favorite = true,
                        }
                    }
                },
            };

            // Combine all contacts into a single list
            Contacts = new List<Contact>();
            foreach (var user in Users)
            {
                Contacts.AddRange(user.Contacts);
            }
        }
    }
}
