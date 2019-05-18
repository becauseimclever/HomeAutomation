using HomeAutomationRepositories.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomationRepositories.DataContext
{
    public interface IMongoContext
    {
        IMongoCollection<RoomEntity> RoomCollection { get; }
    }
}
