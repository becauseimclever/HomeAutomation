using HomeAutomationRepositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationRepositories.Services
{
    public interface IRoomsService
    {
        Task<List<Room>> GetAll();
        Task<Room> GetById(string Id);
        Task Create(Room room);
        Task<bool> AddDevice(string id, Device device);
        Task Delete(string id);
    }
}
