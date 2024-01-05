using Microsoft.AspNetCore.Mvc;
using NetBootcampProject.Models.ORM;

namespace NetBootcampProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly NetBootcampDbContext _context;

        public RoomController()
        {
            _context = new NetBootcampDbContext();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _context.Rooms.ToList();

            return Ok(rooms);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.Id == id);
            if (room == null) { return NotFound(); }

            return Ok(room);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (room == null) { return BadRequest("Room Not Found!"); }

            room.AddDate = DateTime.Now;
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, room);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            if (room == null) { return BadRequest(); }

            var existingRoom = _context.Rooms.Find(id);
            if (existingRoom == null) { return NotFound(); }

            existingRoom.Name = room.Name;

            _context.Update(existingRoom);
            _context.SaveChanges();

            return Ok(existingRoom);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.Id == id);

            if (room == null) { return NotFound(); }

                _context.Rooms.Remove(room);
                _context.SaveChanges();

                return Ok(room);
            }
        }
    }

