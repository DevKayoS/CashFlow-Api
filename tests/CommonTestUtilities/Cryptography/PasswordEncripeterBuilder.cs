using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripeterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("$2y$10$EIm8Hk0U7HbEkLhJKC8qcuUOa2Jt0k7Zhw7JzF2bXYh9qNLJHVuCC");
        
        return mock.Object;
    }
}