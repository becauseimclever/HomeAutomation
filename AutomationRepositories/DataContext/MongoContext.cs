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

namespace BecauseImClever.HomeAutomation.AutomationRepositories.DataContext
{
	using Microsoft.Extensions.Options;
	using MongoDB.Driver;
	using System;
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public class MongoContext<T> : IMongoContext<T> where T : class
	{
		private readonly IMongoDatabase _mongoDatabase;

		public MongoContext(IOptions<MongoSettings> settings)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));
			var client = new MongoClient(settings.Value.ConnectionString);
			_mongoDatabase = client.GetDatabase(settings.Value.Database);
		}
		public IMongoCollection<T> MongoCollection => _mongoDatabase.GetCollection<T>(typeof(T).Name);

	}
	public sealed class MongoContext : IMongoContext
	{
		private readonly IMongoDatabase _mongoDatabase;

		public MongoContext(IOptions<MongoSettings> settings)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));
			var client = new MongoClient(settings.Value.ConnectionString);
			_mongoDatabase = client.GetDatabase(settings.Value.Database);
		}
		IMongoDatabase IMongoContext.MongoDatabase => _mongoDatabase;
	}
}
