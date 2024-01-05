using Microsoft.AspNetCore.Mvc;
using NetBootcampProject.Models.ORM;
using System.Data.Entity;

namespace NetBootcampProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
       private readonly NetBootcampDbContext _context;

        public ReservationController() 
        { 
            
            _context = new NetBootcampDbContext();
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var clients = _context.Clients.Include(x => x.CompanyId).ToList();
            return Ok(clients);
        }


    }
}
