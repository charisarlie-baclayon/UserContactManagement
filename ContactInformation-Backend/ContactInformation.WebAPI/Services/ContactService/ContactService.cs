using AutoMapper;
using ContactInformation.WebAPI.Dtos.Contact;
using ContactInformation.WebAPI.Exceptions;
using ContactInformation.WebAPI.Models;
using ContactInformation.WebAPI.Repositories.ContactRepository;

namespace ContactInformation.WebAPI.Services.ContactService
{
    /// <summary>
    /// Provides method implementation from IContactService.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        /// <summary>
        /// Initializes a new instance of the ContactService class.
        /// </summary>
        /// <param name="mapper">AutoMapper instance for mapping between DTOs and models.</param>
        /// <param name="contactRepository">Repository for data access operations on contacts.</param>
        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }


        /// <inheritdoc/>
        public async Task<int> CreateContact(int userId, ContactCreationDto contactToAdd)
        {
            var newContact = _mapper.Map<Contact>(contactToAdd);
            newContact.UserId = userId;

            var contactId = await _contactRepository.CreateContact(userId, newContact);
            if (contactId == 0)
            {
                throw new ContactCreationFailedException("Contact creation failed.");
            }
            return contactId;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteContact(int userId, int contactId)
        {
            var deleted = await _contactRepository.DeleteContact(userId, contactId);
            if (!deleted)
            {
                throw new ContactDeletionFailedException("Contact deletion failed.");
            }
            return true;
        }

        /// <inheritdoc/>
        public async Task<ContactDto> GetContact(int userId, int contactId)
        {
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException($"Contact with ID {contactId} not found.");
            }
            var newContactDto = _mapper.Map<ContactDto>(contact);
            return newContactDto;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ContactDto>> GetContacts(int userId)
        {
            var contacts = await _contactRepository.GetContacts(userId);
            var contactDtos = _mapper.Map<IEnumerable<ContactDto>>(contacts);
            return contactDtos;
        }

        /// <inheritdoc/>
        public async Task<Contact> UpdateContact(int userId, int contactId, ContactUpdationDto contactToUpdate)
        {
            var existingContact = await _contactRepository.GetContact(userId, contactId);
            if (existingContact == null)
            {
                throw new ContactNotFoundException($"Contact with ID {contactId} not found.");
            }

            var updatedContact = _mapper.Map(contactToUpdate, existingContact);
            updatedContact.Id = contactId;

            var result = await _contactRepository.UpdateContact(userId, updatedContact);
            if (result == null)
            {
                throw new ContactUpdateFailedException("Contact update failed.");
            }
            return result!;
        }

    }
}
