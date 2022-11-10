using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Exceptions;
using Domain.Ports;
using Application.Responses;


namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {
                var room = RoomDto.MapToEntity(request.Data);

                await room.Save(_roomRepository);

                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (InvalidRoomDataException)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information"
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_COULD_NOT_STORE_DATA,
                    Message = "Error to save on DB"
                };
            }
        }

        public Task<RoomResponse> GetRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
