using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAutomationRepositories.Entities;
using HomeAutomationRepositories.Models;
using HomeAutomationRepositories.Repositories;

namespace HomeAutomationRepositories.Services
{
	public class RoomsService : IRoomsService
	{
		private readonly IRoomRepository roomRepo;

		public RoomsService(IRoomRepository roomRepository)
		{
			roomRepo = roomRepository;
		}

		public async Task<List<Room>> GetAll()
		{
			var rooms = await roomRepo.GetAll();
			var roomsList = new List<Room>();
			foreach (var room in rooms)
			{
				roomsList.Add(await ConvertEntitytoModel(room));
			}

			return roomsList;
		}
		private async Task<Room> ConvertEntitytoModel(RoomEntity roomEntity)
		{
			var room = new Room();
			foreach (var device in roomEntity.Devices)
			{
				room.Devices.Add(new Device() { Name = device.Name });
			}
			return room;
		}
	}
}

