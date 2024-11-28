﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contacts.API.Interfaces;
using Contacts.API.Models;
using Contacts.API.Helpers;

namespace Contacts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery] ContactQueryParameters parameters)
        {
            var (contacts, totalRecords) = await _contactService.GetContactsAsync(parameters);

            Response.Headers.Add("X-Total-Count", totalRecords.ToString());

            return Ok(contacts);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contact.ContactID = null; 
            await _contactService.AddOrUpdateContactAsync(contact);

            return CreatedAtAction(nameof(GetContact), new { id = contact.ContactID }, contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (id != contact.ContactID)
            {
                return BadRequest("Contact ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingContact = await _contactService.GetContactByIdAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            await _contactService.AddOrUpdateContactAsync(contact);

            return NoContent();
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var existingContact = await _contactService.GetContactByIdAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            await _contactService.DeleteContactAsync(id);

            return NoContent();
        }
    }
}
