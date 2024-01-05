using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcampProject.Models.ORM;


namespace NetBootcampProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly NetBootcampDbContext _context;
        public ClientController()
        {
            _context = new NetBootcampDbContext();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _context.Clients.Include(x => x.Company).ToList();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _context.Clients.Include(x => x.Company).FirstOrDefault(x => x.Id == id);
            if (client == null) { return NotFound(); }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (client == null) { return BadRequest("Client Not Found!"); }

            Company company = _context.Companies.Find(client.CompanyId);
            if (company == null) { return BadRequest("Company Not Found!"); }

            client.AddDate = DateTime.Now;
            client.Company = company;
            _context.Clients.Add(client);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, client);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Client client)
        {
            if (client == null) { return BadRequest("Client Not Found!"); }

            var existingClient = _context.Clients.Find(id);
            if (existingClient == null) { return NotFound(); }

            existingClient.Name = client.Name;
            existingClient.Surname = client.Surname;
            existingClient.BirthDate = client.BirthDate;
            existingClient.Address = client.Address;
            existingClient.Email = client.Email;

            Company company = _context.Companies.Find(client.CompanyId);
            if (company == null) return BadRequest("Company Not Found!");
            existingClient.CompanyId = company.Id;
            existingClient.Company = company;

            _context.Update(existingClient);
            _context.SaveChanges();

            return Ok(existingClient);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null) { return NotFound(); }

                _context.Clients.Remove(client);
                _context.SaveChanges();

                return Ok(client);
            
        }
    }
}
