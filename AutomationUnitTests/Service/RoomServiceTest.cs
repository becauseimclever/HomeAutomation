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
using AutoFixture;
using AutoFixture.Kernel;
using BecauseImClever.AutomationLogic.Services;
using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationRepositories.Interfaces;
using BecauseImClever.DeviceBase;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BecauseImClever.AutomationUnitTests.Service
{
    public class RoomsServiceTest
    {
        private readonly Mock<IRoomRepository> _mockRoomRepo;
        private Fixture fixture;
        public RoomsServiceTest()
        {
            fixture = new Fixture();
            fixture.Register(() => Guid.NewGuid());
            fixture.Customizations.Add(new TypeRelay(typeof(Device), typeof(GenericDevice)));
            _mockRoomRepo = new Mock<IRoomRepository>();
        }
        [Fact]
        public void ConstuctorThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomService(null));
        }
        [Fact]
        public async Task CreateRoomReturnsRoom()
        {
            var room = fixture.Create<Room>();
            room.Id = Guid.NewGuid();
            _mockRoomRepo.Setup(x => x.CreateRoomAsync(It.IsAny<Room>())).ReturnsAsync(room);
            var roomService = new RoomService(_mockRoomRepo.Object);
            var result = await roomService.CreateAsync(room).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Room>(result);
            Assert.Equal(room.Id, result.Id);
            Assert.Equal(room.Name, result.Name);
        }
        [Fact]
        public async Task GetAllReturnsListOfRooms()
        {
            var rooms = fixture.Create<List<Room>>();
            _mockRoomRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(rooms);

            var roomService = new RoomService(_mockRoomRepo.Object);
            var results = await roomService.GetAllAsync().ConfigureAwait(true);

            Assert.IsType<List<Room>>(results);
            Assert.NotEmpty(results);
            Assert.Equal(rooms.Count, results.Count());
        }
        [Fact]
        public async Task GetByIdReturnsRoom()
        {
            var room = fixture.Create<Room>();
            _mockRoomRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(room);

            var roomsService = new RoomService(_mockRoomRepo.Object);
            var result = await roomsService.GetByIdAsync(room.Id.ToString()).ConfigureAwait(true);

            Assert.NotNull(result);
            Assert.IsType<Room>(result);
            Assert.Equal(room.Id, result.Id);
        }
        [Fact]
        public async Task UpdateReturnsTrue()
        {
            var room = fixture.Create<Room>();
            room.Id = Guid.NewGuid();

            _mockRoomRepo.Setup(x => x.UpdateAsync(It.IsAny<Room>())).ReturnsAsync(true);
            var roomService = new RoomService(_mockRoomRepo.Object);
            var result = await roomService.UpdateAsync(room).ConfigureAwait(true);
        }
        [Fact]
        public async Task DeleteReturnsTrue()
        {
            _mockRoomRepo.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);

            var roomService = new RoomService(_mockRoomRepo.Object);
            var result = await roomService.DeleteAsync("IDSTRING").ConfigureAwait(true);

            Assert.True(result);

        }
    }
}
