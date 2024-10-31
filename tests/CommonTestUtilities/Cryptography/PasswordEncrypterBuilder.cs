using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        
        _mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("$2y$10$EIm8Hk0U7HbEkLhJKC8qcuUOa2Jt0k7Zhw7JzF2bXYh9qNLJHVuCC");
    }

    public PasswordEncrypterBuilder Verify(string password)
    {
        _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>())).Returns(true);
        
        return this;
    }
    
    public IPasswordEncripter Build() => _mock.Object;
  
}