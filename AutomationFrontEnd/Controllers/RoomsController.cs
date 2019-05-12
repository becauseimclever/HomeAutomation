using HomeAutomationRepositories.Authorization;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutomationFrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService _roomsService;
        public RoomsController(IRoomsService roomService)
        {
            _roomsService = roomService;
        }
        [HttpGet]
        [Route("")]
        [MacAuthorize("Admin")]
        public async Task<IActionResult> GetAllRooms(string Id = null)
        {
            if (Id != null)
                return Ok(await _roomsService.GetById(Id));

            return Ok(await _roomsService.GetAll());
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(Room room)
        {
            await _roomsService.Create(room);
            return Ok();
        }
        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> AddDevice(string Id, Device device)
        {
            var room = await _roomsService.AddDevice(Id, device);
            return Ok(room);
        }
    }
}