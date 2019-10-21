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
using BecauseImClever.AutomationRepositories;
using BecauseImClever.AutomationRepositories.DataContext;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BecauseImClever.AutomationUnitTests.Repository
{
	public class PluginRepositoryTest
	{
		public readonly Mock<IMongoDatabase> _mockDatabase;
		private readonly Mock<IMongoContext> _mockContext;
		private readonly Mock<IMongoCollection<Plugin>> _mockPluginCollection;
		private readonly List<Plugin> _pluginList;
		private readonly Plugin _pluginEntity;
		public PluginRepositoryTest()
		{
			_mockContext = new Mock<IMongoContext>();
			_mockPluginCollection = new Mock<IMongoCollection<Plugin>>();
			_pluginEntity = new Plugin()
			{
				Id = Guid.NewGuid(),
				Name = "TestPlugin",
				Path = "/Test/Path"
			};
			_pluginList = new List<Plugin>() { _pluginEntity };
			_mockDatabase = new Mock<IMongoDatabase>();


		}
		[Fact]
		public void CreatePluginRepository()
		{
			_mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockPluginCollection.Object);
			_mockContext.Setup(x => x.MongoDatabase).Returns(_mockDatabase.Object);

			var repo = new PluginRepository(_mockContext.Object);
			Assert.NotNull(repo);
			Assert.IsAssignableFrom<IPluginRepository>(repo);
		}
		[Fact]
		public void CreatePluginRepositoryThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new PluginRepository(null));
		}
		#region Create
		[Fact]
		public async Task CreatePluginReturnsPlugin()
		{
			_mockPluginCollection.Setup(x => x.InsertOneAsync(
				It.IsAny<Plugin>(),
				It.IsAny<InsertOneOptions>(),
				It.IsAny<CancellationToken>()
				)).Returns(Task.FromResult(MongoHelper.BuildMockAsyncCursor((ICollection<Plugin>)_pluginList)));
			_mockDatabase.Setup(x => x.GetCollection<Plugin>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_mockPluginCollection.Object);
			_mockContext.Setup(x => x.MongoDatabase).Returns(_mockDatabase.Object);
			var repo = new PluginRepository(_mockContext.Object);
			var result = await repo.CreatePluginAsync(_pluginEntity).ConfigureAwait(true);
			Assert.NotNull(result);
		}

		#endregion

	}
}
