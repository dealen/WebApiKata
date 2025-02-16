using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ToDoApi.Controllers;
using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.ResponseModels;
using Xunit;  // Or NUnit, MSTest - whichever you prefer

public class AuthControllerTests
{
    [Fact]
    public void Login_ValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<AuthController>>();
        var mockAuthService = new Mock<IAuthService>();
        const string expectedToken = "TestToken";
        mockAuthService.Setup(service => service.Authenticate("test", "password"))
                       .Returns(expectedToken);

        var controller = new AuthController(mockLogger.Object, mockAuthService.Object);
        var userModel = new UserModel { UserName = "test", Password = "password" };

        // Act
        var result = controller.Login(userModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var tokenResult = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal(expectedToken, tokenResult.Token);
        mockAuthService.Verify(service => service.Authenticate("test", "password"), Times.Once);
    }

    [Fact]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<AuthController>>();
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns((string)null);

        var controller = new AuthController(mockLogger.Object, mockAuthService.Object);
        var userModel = new UserModel { UserName = "invalid", Password = "invalid" };

        // Act
        var result = controller.Login(userModel);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
         mockAuthService.Verify(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
     [Fact]
    public void Login_NullCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<AuthController>>();
        var mockAuthService = new Mock<IAuthService>();
      

        var controller = new AuthController(mockLogger.Object, mockAuthService.Object);

        // Act
        var result = controller.Login(null);

        // Assert
        Assert.IsType<UnauthorizedResult>(result); // Expect Unauthorized
        mockAuthService.Verify(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Never); // Ensure the service was *not* called
    }
}