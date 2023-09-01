using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test.TestUtils
{
    public static class MockHelper
    {
        public static IUserContext GetMockUserContext(long userId)
        {
            var mockUserContext = new Mock<IUserContext>();
            mockUserContext.Setup(x => x.CurrentUserId).Returns(userId);
            return mockUserContext.Object;
        }
     
    }
}
