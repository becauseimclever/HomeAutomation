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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }
        #region Create
        [HttpPost]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public async ValueTask<IActionResult> CreateAsync([Required] Group group)
        {
            var newRoom = await _groupService.CreateAsync(group).ConfigureAwait(false);
            return Ok(newRoom);
        }
        #endregion
        #region Read
        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var groups = await _groupService.GetAllAsync().ConfigureAwait(false);
            return Ok(groups);
        }
        [HttpGet]
        [Route("{Id}")]
        public async ValueTask<IActionResult> GetByIdAsync([Required] string Id)
        {
            return Ok(await _groupService.GetByIdAsync(Id).ConfigureAwait(false));
        }
        #endregion
        #region Update
        [HttpPut]
        [Route("")]
        public async ValueTask<IActionResult> UpdateAsync([Required] Group group)
        {
            var updateGroup = await _groupService.UpdateAsync(group).ConfigureAwait(false);
            return Ok(updateGroup);
        }
        #endregion
        #region Delete
        [HttpDelete]
        [Route("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([Required] string Id)
        {
            bool success = await _groupService.DeleteAsync(Id).ConfigureAwait(false);
            if (success)
                return NoContent();
            else
                return BadRequest();

        }
        #endregion

    }
}