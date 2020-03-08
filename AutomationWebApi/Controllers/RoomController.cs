//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
//	Copyright(C) 2019 Darren Swan
//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.
//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//	GNU General Public License for more details.
//	You should have received a copy of the GNU General Public License
//	along with this program.If not, see<https://www.gnu.org/licenses/>.


namespace BecauseImClever.HomeAutomation.AutomationWebApi.Controllers
{
    using Abstractions;
    using AutomationModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMessageService _messageService;
        public RoomController(IRoomService roomService, IMessageService messageService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }
        #region Create
        [HttpPost]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public async ValueTask<IActionResult> CreateAsync([Required]Room room)
        {
            var newRoom = await _roomService.CreateAsync(room);
            _ = _messageService.Enqueue($"{room.Name} - {room.Id} was created");
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
            return Ok(await _roomService.GetByIdAsync(Id));
        }
        [HttpPut]
        [Route("")]
        public async ValueTask<IActionResult> UpdateAsync(Room room)
        {
            var updateRoom = await _roomService.UpdateAsync(room);
            return Ok(updateRoom);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(string Id)
        {
            bool success = await _roomService.DeleteAsync(Id);
            if (success)
                return NoContent();
            else
                return BadRequest();

        }
    }
}