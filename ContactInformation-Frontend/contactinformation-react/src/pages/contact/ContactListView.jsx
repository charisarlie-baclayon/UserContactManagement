import React from "react";

const ContactListView = () => {
  const contacts = [
    {
      firstName: "John",
      lastName: "Doe",
      addresses: [
        {
          addressDescription: "123 Main St, Example City, CA 12345",
          addressType: "Home",
        },
      ],
      phoneNumber: "555-123-4567",
      emailAddress: "john.doe@example.com",
      birthDate: new Date(1990, 5, 15),
      favorite: true,
      imageUrl: "https://example.com/john_doe.jpg",
    },
    {
      firstName: "Jane",
      lastName: "Smith",
      addresses: [
        {
          addressDescription: "456 Elm St, Sample Town, NY 67890",
          addressType: "Work",
        },
      ],
      phoneNumber: "555-987-6543",
      emailAddress: "jane.smith@example.com",
      birthDate: new Date(1988, 8, 22),
      favorite: false,
      imageUrl: "https://example.com/jane_smith.jpg",
    },
    {
      firstName: "Michael",
      lastName: "Johnson",
      addresses: [
        {
          addressDescription: "789 Oak Ave, Testville, TX 54321",
          addressType: "Home",
        },
      ],
      phoneNumber: "555-555-5555",
      emailAddress: "michael.johnson@example.com",
      birthDate: new Date(1995, 2, 10),
      favorite: true,
      imageUrl: "https://example.com/michael_johnson.jpg",
    },
    {
      firstName: "Emily",
      lastName: "Williams",
      addresses: [
        {
          addressDescription: "101 Maple Rd, Another City, IL 98765",
          addressType: "Work",
        },
      ],
      phoneNumber: "555-123-7890",
      emailAddress: "emily.williams@example.com",
      birthDate: new Date(1992, 10, 8),
      favorite: true,
      imageUrl: "https://example.com/emily_williams.jpg",
    },
    {
      firstName: "David",
      lastName: "Brown",
      addresses: [
        {
          addressDescription: "222 Pine St, Different Town, CA 54321",
          addressType: "Home",
        },
      ],
      phoneNumber: "555-789-1234",
      emailAddress: "david.brown@example.com",
      birthDate: new Date(1985, 7, 17),
      favorite: false,
      imageUrl: "https://example.com/david_brown.jpg",
    },
    {
      firstName: "Olivia",
      lastName: "Davis",
      addresses: [
        {
          addressDescription: "333 Cedar Rd, Sample City, TX 67890",
          addressType: "Work",
        },
      ],
      phoneNumber: "555-567-8901",
      emailAddress: "olivia.davis@example.com",
      birthDate: new Date(1993, 3, 25),
      favorite: true,
      imageUrl: "https://example.com/olivia_davis.jpg",
    },
    {
      firstName: "William",
      lastName: "Miller",
      addresses: [
        {
          addressDescription: "444 Oak Ave, Different City, NY 12345",
          addressType: "Home",
        },
      ],
      phoneNumber: "555-456-7890",
      emailAddress: "william.miller@example.com",
      birthDate: new Date(1991, 11, 5),
      favorite: false,
      imageUrl: "https://example.com/william_miller.jpg",
    },
    {
      firstName: "Sophia",
      lastName: "Wilson",
      addresses: [
        {
          addressDescription: "555 Maple Rd, Another Town, CA 67890",
          addressType: "Work",
        },
      ],
      phoneNumber: "555-234-5678",
      emailAddress: "sophia.wilson@example.com",
      birthDate: new Date(1987, 1, 30),
      favorite: true,
      imageUrl: "https://example.com/sophia_wilson.jpg",
    },
    {
      firstName: "James",
      lastName: "Moore",
      addresses: [
        {
          addressDescription: "666 Pine St, Sample City, TX 12345",
          addressType: "Home",
        },
      ],
      phoneNumber: "555-678-9012",
      emailAddress: "james.moore@example.com",
      birthDate: new Date(1984, 9, 12),
      favorite: false,
      imageUrl: "https://example.com/james_moore.jpg",
    },
    {
      firstName: "Isabella",
      lastName: "Taylor",
      addresses: [
        {
          addressDescription: "777 Cedar Rd, Different City, NY 67890",
          addressType: "Work",
        },
      ],
      phoneNumber: "555-789-0123",
      emailAddress: "isabella.taylor@example.com",
      birthDate: new Date(1989, 6, 3),
      favorite: true,
      imageUrl: "https://example.com/isabella_taylor.jpg",
    },
  ];
  return (
    <div>
      <h1 class="text-whiterText">Your Contacts</h1>

      <ul role="list" class="divide-y divide-greyBorder">
        {contacts.map((contact) => (
          <li
            key={contact.emailAddress}
            class="flex justify-between gap-x-6 py-5"
          >
            <div class="flex min-w-0 gap-x-4">
              <img
                class="h-12 w-12 flex-none rounded-full bg-gray-50"
                src="../src/assets/people.png"
                alt=""
              />
              <div class="min-w-0 flex-auto">
                <p class="text-sm font-semibold leading-6 text-whiterText">
                  {contact.firstName} {contact.lastName}
                </p>
                <p class="mt-1 truncate text-xs leading-5 text-gray-500">
                  {contact.emailAddress}
                </p>
              </div>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ContactListView;
