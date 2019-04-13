using HomeAutomationRepositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Repositories
{
	public interface IRoomRepository
	{
		Task<List<RoomEntity>> GetAll();
	}
}
