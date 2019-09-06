using BecauseImClever.AutomationLogic.Interfaces;
using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlazorDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        }
        #region Create
        [HttpPost]
        [Route("")]
        public async ValueTask<IActionResult> CreateAsync(Room room)
        {
            var newRoom = await _roomService.CreateAsync(room);
            return Ok(newRoom);
        }
        #endregion

        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
        }
        [HttpGet]
        [Route("{Id}")]
        public async ValueTask<IActionResult> GetByIdAsync(string Id)
        {
            var room = await _roomService.GetByIdAsync(Id);
            return Ok(room);
        }
    }
}