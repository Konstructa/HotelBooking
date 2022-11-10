using Domain.Enums;
using Entities = Domain.Entities;

namespace Application.Booking.Dtos
{
    public class BookingDto
    {
        public BookingDto()
        {
            this.PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        private Status Status { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public static Entities.Booking MapToEntity(BookingDto bookingDto)
        {
            return new Entities.Booking
            {
                Id = bookingDto.Id,
                Start = bookingDto.Start,
                Guest = new Entities.Guest { Id = bookingDto.RoomId },
                Room = new Entities.Room { Id = bookingDto.GuestId },
                End = bookingDto.End,
                PlacedAt = bookingDto.PlacedAt,
            };
        }
    }
}
