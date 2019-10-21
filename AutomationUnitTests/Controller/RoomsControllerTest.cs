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
using AutomationWebApi.Controllers;
using BecauseImClever.Abstractions;
using BecauseImClever.AutomationModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BecauseImClever.AutomationUnitTests.Controller
{
	public class RoomsControllerTest
	{
		private readonly Mock<IRoomService> _mockService;
		private readonly Fixture _fixture;
		public RoomsControllerTest()
		{
			_mockService = new Mock<IRoomService>();
			_fixture = new Fixture();
		}
		[Fact]
		public void CreateRoomController()
		{
			var controller = new RoomController(_mockService.Object);
			Assert.NotNull(controller);
		}
		[Fact]
		public void CreateRoomControllerThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var controller = new RoomController(null);
			});
		}
		[Fact]
		public async ValueTask CreateAsyncReturnsRoom()
		{
			var newRoom = _fixture.Create<Room>();

			_mockService.Setup(x => x.CreateAsync(It.IsAny<Room>())).ReturnsAsync(newRoom);

			var controller = new RoomController(_mockService.Object);
			var returnedRoom = await controller.CreateAsync(newRoom);
			Assert.NotNull(returnedRoom);
			var room = returnedRoom as OkObjectResult;
			var roomObject = JsonSerializer.Deserialize<Room>(room.Value.ToString());

			Assert.Equal(newRoom.Id, roomObject.Id);
		}
		[Fact]
		public async ValueTask GetAllAsyncReturnsCollection()
		{
			var roomList = _fixture.Create<IEnumerable<Room>>();
			_mockService.Setup(x => x.GetAllAsync()).ReturnsAsync(roomList);
			var controller = new RoomController(_mockService.Object);
			var result = await controller.GetAllAsync();
			var resultObject = result as OkObjectResult;
			var roomsList = JsonSerializer.Deserialize<IEnumerable<Room>>(resultObject.Value.ToString());
			Assert.NotEmpty(roomsList);
		}
	}
}
