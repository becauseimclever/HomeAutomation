﻿//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on rooms and devices.
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BecauseImClever.AutomationModels;
using BecauseImClever.Abstractions;

namespace BecauseImClever.AutomationLogic.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }
        public async ValueTask<Room> CreateAsync(Room room)
        {
            return await _roomRepository.CreateRoomAsync(room);
        }
        public async ValueTask<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async ValueTask<Room> GetByIdAsync(string Id)
        {
            return await _roomRepository.GetByIdAsync(Guid.Parse(Id)).ConfigureAwait(false);
        }
        public async ValueTask<bool> UpdateAsync(Room room)
        {
            return await _roomRepository.UpdateAsync(room).ConfigureAwait(false);
        }
        public async ValueTask<bool> DeleteAsync(string Id)
        {
            return await _roomRepository.DeleteAsync(Id).ConfigureAwait(false);
        }
    }
}
