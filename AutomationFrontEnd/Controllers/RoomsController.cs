using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutomationFrontEnd.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService _roomsService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomService"></param>
        public RoomsController(IRoomsService roomService)
        {
            _roomsService = roomService;
        }
        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>Room</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(Room room)
        {
            await _roomsService.CreateAsync(room).ConfigureAwait(true);
            return Ok();
        }
        #endregion
        #region Read
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of all rooms</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllRooms(string Id = null)
        {
            if (Id != null)
                return Ok(await _roomsService.GetByIdAsync(Id).ConfigureAwait(true));

            return Ok(await _roomsService.GetAllAsync().ConfigureAwait(true));
        }

        #endregion
        #region Update

        #endregion

        #region Delete
        #endregion
    }
}