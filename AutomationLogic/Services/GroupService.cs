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
namespace BecauseImClever.HomeAutomation.AutomationLogic.Services
{

    using Abstractions;
    using AutomationModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _roomRepository;
        public GroupService(IGroupRepository roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }
        public async ValueTask<Group> CreateAsync(Group group)
        {
            return await _roomRepository.CreateAsync(group);
        }
        public async ValueTask<IEnumerable<Group>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async ValueTask<Group> GetByIdAsync(string Id)
        {
            return await _roomRepository.GetByIdAsync(Guid.Parse(Id)).ConfigureAwait(false);
        }
        public async ValueTask<bool> UpdateAsync(Group group)
        {
            return await _roomRepository.UpdateAsync(group).ConfigureAwait(false);
        }
        public async ValueTask<bool> DeleteAsync(string id)
        {
            return await _roomRepository.DeleteAsync(Guid.Parse(id)).ConfigureAwait(false);
        }
    }
}