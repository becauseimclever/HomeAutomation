using HomeAutomationRepositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
	public class RoomRepository : IRoomRepository
	{

		public RoomRepository()
		{

		}

		public async Task<List<RoomEntity>> GetAll()
		{
			return await Task.FromResult(new List<RoomEntity>());
		}
	}
}
