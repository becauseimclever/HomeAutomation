using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationRepositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> GetAllRooms()
		{
			return Ok(await _roomsService.GetAll());
		}
	}
}