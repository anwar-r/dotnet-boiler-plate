

namespace Data.Test.Util
{

    public class UserContextTests
    {
        private IUserContext _userContext;

        [SetUp]
        public void Setup()
        {
            var claims = new List<Claim>()
            {
                new("UserId", "1"),
            };

            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var context = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();

            mockHttpAccessor.Setup(x => x.HttpContext).Returns(context);
            _userContext = new UserContext(mockHttpAccessor.Object);    
        }

        [Test]
        public void ShouldGetUserIdFromHttpContext()
        {
            Assert.That(_userContext.CurrentUserId, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNotGetUserIdFromHttpContextWhenClaimsAreNotPresent()
        {
            var context = new DefaultHttpContext();

            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            mockHttpAccessor.Setup(x => x.HttpContext).Returns(context);
            _userContext = new UserContext(mockHttpAccessor.Object);
            Assert.That(_userContext.CurrentUserId, Is.EqualTo(0));
        }
    }
}
