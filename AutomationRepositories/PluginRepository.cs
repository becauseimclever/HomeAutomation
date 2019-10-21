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
using BecauseImClever.Abstractions;
using BecauseImClever.AutomationModels;
using BecauseImClever.AutomationRepositories.DataContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.AutomationRepositories
{
	public class PluginRepository : IPluginRepository
	{
		private IMongoCollection<Plugin> _pluginCollection;
		public PluginRepository(IMongoContext mongoContext)
		{
			var _context = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
			_pluginCollection = _context.MongoDatabase.GetCollection<Plugin>($"{nameof(Plugin)}s", new MongoCollectionSettings() { GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard });
		}
		#region Create   
		public async ValueTask<Plugin> CreatePluginAsync(Plugin plugin)
		{
			var _plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
			await _pluginCollection.InsertOneAsync(_plugin).ConfigureAwait(true);
			return _plugin;
		}
		#endregion

		#region Read
		public async ValueTask<Plugin> GetPluginAsync(Guid Id)
		{
			var builder = Builders<Plugin>.Filter;
			var filter = builder.Eq(x => x.Id, Id);
			return await _pluginCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(true);
		}
		public async ValueTask<IEnumerable<Plugin>> GetPluginsAsync()
		{
			var filter = Builders<Plugin>.Filter.Empty;
			var results = await _pluginCollection.FindAsync(filter).ConfigureAwait(true);

			return await results.ToListAsync().ConfigureAwait(true);
		}
		#endregion
		#region Update
		public ValueTask<Plugin> UpdatePluginAsync(Plugin plugin)
		{
			throw new NotImplementedException();
		}
		#endregion
		#region Delete
		public ValueTask<bool> DetelePluginAsync(Guid Id)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
