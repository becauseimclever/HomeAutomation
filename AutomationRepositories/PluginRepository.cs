//	HomeAutomation - Home Automation system in .NET Core and Blazor, focusing on plugins and devices.
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


namespace BecauseImClever.HomeAutomation.AutomationRepositories
{
    using Abstractions;
    using AutomationModels;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class PluginRepository : IPluginRepository
    {
        private IMongoCollection<Plugin> _pluginCollection;
        public PluginRepository(IMongoDatabase mongoDatabase)
        {
            var _mongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
            _pluginCollection = mongoDatabase.GetCollection<Plugin>($"{nameof(Plugin)}s", new MongoCollectionSettings() { GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard });
        }
        #region Create   
        public async ValueTask<Plugin> CreateAsync(Plugin plugin)
        {
            var _plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
            await _pluginCollection.InsertOneAsync(_plugin).ConfigureAwait(true);
            return _plugin;
        }
        public async ValueTask<IEnumerable<Plugin>> CreateManyAsync(IEnumerable<Plugin> plugins)
        {
            await _pluginCollection.InsertManyAsync(plugins).ConfigureAwait(false);
            return plugins;
        }
        #endregion

        #region Read
        public async ValueTask<Plugin> GetByIdAsync(Guid Id)
        {
            var builder = Builders<Plugin>.Filter;
            var filter = builder.Eq(x => x.Id, Id);
            return await _pluginCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(true);
        }
        public async ValueTask<IEnumerable<Plugin>> GetAllAsync()
        {
            var filter = Builders<Plugin>.Filter.Empty;
            var results = await _pluginCollection.FindAsync(filter).ConfigureAwait(true);

            return await results.ToListAsync().ConfigureAwait(true);
        }
        #endregion
        #region Update
        public async ValueTask<bool> UpdateAsync(Plugin plugin)
        {
            var builder = Builders<Plugin>.Filter;
            var filter = builder.Eq(x => x.Id, plugin?.Id);

            var update = Builders<Plugin>.Update
                .Set(x => x.Name, plugin?.Name)
                .Set(x => x.Path, plugin?.Path);
            var updateOptions = new UpdateOptions() { IsUpsert = false };
            var returnValue = await _pluginCollection.UpdateOneAsync(filter, update, updateOptions).ConfigureAwait(false);
            return returnValue.IsAcknowledged;
        }
        #endregion
        #region Delete
        public async ValueTask<bool> DeleteAsync(Guid Id)
        {
            var returnValue = await _pluginCollection.DeleteOneAsync(x => x.Id == Id).ConfigureAwait(true);

            return returnValue.IsAcknowledged;
        }

        public async ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> Ids)
        {
            var builder = Builders<Plugin>.Filter;
            var filter = builder.In(x => x.Id, Ids);
            var returnValue = await _pluginCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            return (returnValue.IsAcknowledged, returnValue.DeletedCount);
        }
        #endregion
    }
}
