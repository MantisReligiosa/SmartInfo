using NSubstitute;
using NUnit.Framework;
using ServiceInterfaces;
using Services;

namespace BusinessLogicTests
{
    [TestFixture]
    public class AccountControllerTest
    {
        private IAccountController _accountController;
        private ICryptoProvider _cryptoProvider;
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _cryptoProvider = Substitute.For<ICryptoProvider>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            _unitOfWorkFactory.Create().Returns(_unitOfWork);

            _accountController = new AccountController(_cryptoProvider, _unitOfWorkFactory);
        }

        [TestCase("", ExpectedResult = false)]
        [TestCase("2", ExpectedResult = false)]
        [TestCase("в", ExpectedResult = false)]
        [TestCase("1234", ExpectedResult = false)]
        [TestCase("12345", ExpectedResult = true)]
        [TestCase("123456", ExpectedResult = true)]
        [TestCase("1234", ExpectedResult = false)]
        [TestCase("c2h5OH05*", ExpectedResult = true)]
        [TestCase("c2h5OH05*привет", ExpectedResult = false)]
        [TestCase("1привет$$", ExpectedResult = false)]
        public bool PasswordTest(string password)
        {
            return _accountController.IsNewPasswordValid(password);
        }

        [TestCase("", ExpectedResult = true)]
        [TestCase("2", ExpectedResult = false)]
        [TestCase("в", ExpectedResult = false)]
        [TestCase("1234", ExpectedResult = false)]
        [TestCase("12345", ExpectedResult = true)]
        [TestCase("123456", ExpectedResult = true)]
        [TestCase("1234", ExpectedResult = false)]
        [TestCase("$uperUser1", ExpectedResult = true)]
        [TestCase("$uperUser1*привет", ExpectedResult = false)]
        [TestCase("1юзер$$", ExpectedResult = false)]
        public bool NewLoginTest(string newLogin)
        {
            return _accountController.IsNewLoginValid(newLogin);
        }

    }
}
