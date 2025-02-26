using NUnit.Framework;
using APITestAutomation.APIutility;
using APITestAutomation.Models;

namespace APITestAutomation
{
    [TestFixture]
    public class APITests
    {
        private const string BaseUrl = "https://dummyjson.com";
        private APIUtility _apiUtility;

        [SetUp]
        public void Setup()
        {
            _apiUtility = new APIUtility(BaseUrl);
        }

        [Test]
        public void APIInvalidLoginTest()
        {
            // Arrange
            var endpoint = "/auth/login";
            var requestBody = new LoginRequest
            {
                Username = "user",
                Password = "invalidCreds"
            };

            // Act
            var response = _apiUtility.Post<LoginResponse, LoginRequest>(endpoint, requestBody);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Username and password required", response.Message);
        }
    }
}