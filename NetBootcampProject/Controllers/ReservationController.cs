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
        public IActionResult GetAll()
        {
            var reservations = _context.Reservations.Include(x => x.Room).Include(x => x.Client.Company).ToList();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = _context.Reservations.Include(x => x.Room).Include(x => x.Client.Company).FirstOrDefault(x => x.Id == id);
            if (reservation == null) { return NotFound(); }

            return Ok(reservation);
        }


        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if (reservation == null)
                return BadRequest("Reservation Not Found!");

            reservation.AddDate = DateTime.Now;
            Client client = _context.Clients.Find(reservation.ClientId);
            Room room = _context.Rooms.Find(reservation.RoomId);

            if (room == null || client == null) { return BadRequest(); }

            reservation.Room = room;
            reservation.RoomId = room.Id;

            reservation.Client = client;
            reservation.ClientId = client.Id;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation reservation)
        {
            var existingReservation = _context.Reservations.FirstOrDefault(r => r.Id == id);

            if (existingReservation == null) { return NotFound(); }

            Client client = _context.Clients.Find(reservation.ClientId);
            Room room = _context.Rooms.Find(reservation.RoomId);

            if (room == null || client == null) { return BadRequest(); }

            existingReservation.Room = room;
            existingReservation.RoomId = room.Id;
            existingReservation.Client = client;
            existingReservation.ClientId = client.Id;

            existingReservation.ReservationDate = reservation.ReservationDate;
            existingReservation.ReservationEndDate = reservation.ReservationEndDate;
            existingReservation.RoomId = reservation.RoomId;
            existingReservation.ClientId = reservation.ClientId;

            _context.Reservations.Update(existingReservation);
            _context.SaveChanges();

            return Ok(existingReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null) { return NotFound(); }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return Ok(reservation);
        }


    }
}
