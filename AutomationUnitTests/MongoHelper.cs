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
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace BecauseImClever.AutomationUnitTests
{
    [ExcludeFromCodeCoverage]
    public static class MongoHelper
    {
        public static IAsyncCursor<T> BuildMockAsyncCursor<T>(T expectedResult)
        {
            var list = new List<T> { expectedResult };
            return BuildMockAsyncCursor((ICollection<T>)list);
        }
        public static IAsyncCursor<T> BuildMockAsyncCursor<T>(ICollection<T> expectedResult)
        {
            var mockCursor = new Mock<IAsyncCursor<T>>();
            mockCursor.Setup(_ => _.Current).Returns(expectedResult);
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);
            return mockCursor.Object;

        }
    }
}
