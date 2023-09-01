using Data.util.Cryptography;

namespace Data.Test.Util
{
    public class PasswordHasherTests
    {
        [Test]
        public void ShouldHashProvidedPassword()
        {
            const string password = "123456";
            IPasswordHasher passwordHasher = new PasswordHasher();

            var hashedPassword = passwordHasher.Hash(password);

            Assert.That(hashedPassword, Is.Not.EqualTo(password));
            Assert.That(hashedPassword.Split('.'), Has.Length.EqualTo(3));
        }

        [Test]
        public void ShouldVerifyHashedPasswordAndReturnTrueWhenHashMatches()
        {
            const string password = "123654";
            IPasswordHasher passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.Hash(password);

            var result = passwordHasher.Check(hashedPassword, password);
            Assert.That(result.Verified, Is.True);
        }
        [Test]
        public void ShouldVerifyHashedPasswordReturnFalseWhenHashDoesntMatches()
        {
            const string password = "123654";
            IPasswordHasher passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.Hash(password);

            var result = passwordHasher.Check(hashedPassword, "wrongPassword");
            Assert.That(result.Verified, Is.False);
        }
        [Test]
        public void ShouldThrowExceptionWhileVerifyingWhenHashIsNotInExpectedFormat()
        {
            const string password = "123654";
            IPasswordHasher passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.Hash(password);
            var wrongHash = hashedPassword.Split('.')[1];
            Assert.Throws<FormatException>(() => passwordHasher.Check(wrongHash, password));
        }
    }
}
