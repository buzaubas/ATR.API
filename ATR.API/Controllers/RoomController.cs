using ATR.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RoomController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> Get()
        {
            return await dbContext.Rooms.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Room room) //FromBody - из RAW текста, FromForm - из формы с инпутами
        {
            try
            {
                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();
                return Ok(room);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);  
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm]Room room)
        {
            var foundRoom = dbContext.Rooms.Find(room.RoomId);
            if(foundRoom == null)
            {
                return BadRequest("Room does not exist");
            }
            else
            {
                try
                {
                    foundRoom.Description = room.Description;
                    foundRoom.Price = room.Price;
                    foundRoom.CreateDate = room.CreateDate;
                    foundRoom.CreateUser = room.CreateUser;
                    foundRoom.IsAvalible = room.IsAvalible;
                    foundRoom.RoomNumber = room.RoomNumber;
                    foundRoom.CategoryId = room.CategoryId;
                    foundRoom.MainPicturePath = room.MainPicturePath;
                    dbContext.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                    return BadRequest(e.Message);
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int RoomId)
        {
            try
            {
                var foundRoom = dbContext.Rooms.Find(RoomId);
                dbContext.Rooms.Remove(foundRoom);
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
