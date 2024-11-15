using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyWarehouse.Infrastructure.Authentication.Core.Model;
using MyWarehouse.Infrastructure.Authentication.Core.Services;
using MyWarehouse.WebApi.API.V1;
using MyWarehouse.WebApi.Authentication.Models.Dtos;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MyWarehouse.WebApi.UnitTests.API.V1
{
    public class AccountControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private AccountController _sut;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>(MockBehavior.Strict);
            _sut = new AccountController(_mockUserService.Object);
        }

        [Test]
        public async Task Login_Successful_ReturnsLoginData()
        {
            var loginDto = new LoginDto() { Username = "User", Password = "1234" };
            var signInResponse = MySignInResult.Success;
            var signInData = new SignInData { Token = new TokenModel("Bearer", "veryFancyToken", DateTime.Now.AddDays(5)) };
            _mockUserService.Setup(x => x.SignIn(loginDto.Username, loginDto.Password))
                .ReturnsAsync((signInResponse, signInData));

            var result = await _sut.Login(loginDto);
            var resultAlt = await _sut.LoginForm(loginDto);

            result.Should().BeEquivalentTo(resultAlt);
            result.Result.Should().BeAssignableTo(typeof(OkObjectResult));
            var tokenResult = (LoginResponseDto)((OkObjectResult)result.Result).Value;
            tokenResult.TokenType.Should().Be(signInData.Token.TokenType);
            tokenResult.AccessToken.Should().Be(signInData.Token.AccessToken);
            tokenResult.Username.Should().Be(signInData.Username);
            tokenResult.ExpiresIn.Should().BePositive();
        }

        [Test]
        public async Task Login_Unsuccessful_ReturnsUnauthorized()
        {
            var loginDto = new LoginDto() { Username = "User", Password = "1234" };
            var signInResponse = MySignInResult.Failed;
            _mockUserService.Setup(x => x.SignIn(loginDto.Username, loginDto.Password))
                .ReturnsAsync((signInResponse, null));

            var result = await _sut.Login(loginDto);

            result.Result.Should().BeAssignableTo(typeof(UnauthorizedObjectResult));
        }

        [Test]
        public async Task Login_UserLockedOut_ReturnForbid()
        {
            var loginDto = new LoginDto() { Username = "User", Password = "1234" };
            var signInResponse = MySignInResult.LockedOut;
            _mockUserService.Setup(x => x.SignIn(loginDto.Username, loginDto.Password))
                .ReturnsAsync((signInResponse, null));

            var result = await _sut.Login(loginDto);

            result.Result.Should().BeAssignableTo(typeof(ForbidResult));
        }

        [Test]
        public async Task Login_UserNotAllowed_ReturnForbid()
        {
            var loginDto = new LoginDto() { Username = "User", Password = "1234" };
            var signInResponse = MySignInResult.NotAllowed;
            _mockUserService.Setup(x => x.SignIn(loginDto.Username, loginDto.Password))
                .ReturnsAsync((signInResponse, null));

            var result = await _sut.Login(loginDto);

            result.Result.Should().BeAssignableTo(typeof(ForbidResult));
        }
    }
}