using Hotel_Room_Booking_system.Models;
using System.Linq;

namespace Hotel_Room_Booking_system.Repositories
{
    public class RoomRepo:BaseRepo<Room>, IRoomRepo
    {
        private readonly HotelContext _context;
        private readonly IMapper _mapper;

        public RoomRepo(HotelContext hotelContext, IMapper mapper) : base(hotelContext)
        {
            _mapper = mapper;
            _context = hotelContext;    
        }
        public async Task<List<ReturmedRoomsDTO>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            // Get room IDs that are booked during the specified date range
            var bookedRoomIds = await _context.Bookings
                .Where(b => b.CheckIn < checkOut && b.CheckOut > checkIn && b.Status == RoomStatus.Available.ToString()&&b.Status==RoomStatus.Available.ToString())
                .Select(b => b.RoomId)
                .ToListAsync();

            //Check Season
            var oldRrooms = await _context.Rooms
           .Where(r => !bookedRoomIds.Contains(r.RoomId)).ToListAsync();
            var rooms= await CalculateSeasonPercantge(oldRrooms, checkIn, checkOut);
            return rooms;
        }

        public async Task<List<ReturmedRoomsDTO>> GetAllRoomsAsync()
        {
            var oldRrooms = await GetAllAsync();

            var rooms = await CalculateSeasonPercantge(oldRrooms, DateTime.Now, DateTime.Now.AddDays(1));

            return rooms;
        }

        public async Task<Room> GetRoomDetailsAsync(int roomId) => await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == roomId);

        public async Task<List<ReturmedRoomsDTO>> GetRoomsByPriceRangeAsync(double minPrice, double maxPrice)
        {

           var old = await _context.Rooms
                .Where(r => r.BasePrice >= minPrice && r.BasePrice <= maxPrice && r.Status == RoomStatus.Available.ToString())
                .ToListAsync();
           return await CalculateSeasonPercantge(old, DateTime.Now, DateTime.Now.AddDays(1));
        }

        public async Task<List<ReturmedRoomsDTO>> GetRoomsByCapacityAsync(int capacity)
        {
            var old = await _context.Rooms
                .Where(r => r.Capacity == capacity && r.Status == RoomStatus.Available.ToString())
                .ToListAsync();
           return await CalculateSeasonPercantge(old, DateTime.Now, DateTime.Now.AddDays(1));
        }

        public async Task AddRoomAsync(RoomDTO dTO)
        {
            var room = _mapper.Map<Room>(dTO);
            await CreateAsync(room);
        }

        public async Task<(bool isSeason,double Percentage)> CheckSeasonAsync(DateTime checkIn, DateTime checkOut) {

            var AllSeasonalDates = await _context.SeasonalPrices.ToListAsync();

            foreach (var date in AllSeasonalDates)
            {
                if (date.IsActive(checkIn, checkOut))
                {
                    return (true,(double)date.IncreasePercentage);
                }
            }
            return (false,1);
        }

        public async Task<List<ReturmedRoomsDTO>> CalculateSeasonPercantge(List<Room> room, DateTime checkIn, DateTime checkOut)
        {
            var season = await CheckSeasonAsync(checkIn, checkOut);
            var days = (checkOut - checkIn).TotalDays;

            var rooms = new List<ReturmedRoomsDTO>();
            foreach (var r in room)
            {
                var roomDto = _mapper.Map<ReturmedRoomsDTO>(r);
                roomDto.TotalPrice = Math.Round(season.isSeason ? r.BasePrice * days * (1 + season.Percentage) : r.BasePrice, 2);
                rooms.Add(roomDto);
            }

            return rooms;
        }
        

    }
}
