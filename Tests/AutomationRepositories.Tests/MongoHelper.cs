using BecauseImClever.AutomationModels;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutomationRepositories.Tests
{
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