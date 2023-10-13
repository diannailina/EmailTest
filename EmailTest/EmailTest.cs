using System;
using System.Globalization;
using System.Text;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using EmailTest.PageObjects;
using EmailTest.SeleniumHelpers;
using EmailTest.Utilities;

namespace EmailTest
{
    [TestFixture]
    public class EmailTest
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;

        private string _loginEmail = ConfigurationHelper.Get<string>("LoginEmail");
        private string _password = ConfigurationHelper.Get<string>("Password");

        [SetUp]
        public void SetupTest()
        {
            _driver = new DriverFactory().Create();
            _baseUrl = ConfigurationHelper.Get<string>("TargetUrl");
            _verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
                _driver.Close();
            }
            catch (Exception)
            {
                // Ignore errors if we are unable to close the browser
            }
            _verificationErrors.ToString().Should().BeEmpty("No verification errors are expected.");
        }

        [Test]
        public void TestEmailSending()
        {
            // Arrange
            // Act
            new LoginPage(_driver).LoginAsUser(_baseUrl, _loginEmail, _password);

            new MainPage(_driver).SendAnEmail(_loginEmail);

            new MainPage(_driver).LogOut();
            
            // Assert
             new LoginPage(_driver).LoginButton.Displayed.Should().BeTrue();
        }

        [Test]
        public void RunBrowserTest(){}

        [Test]
        public void TestEmailReceiving()
        {
            // Arrange
            // Act
            new LoginPage(_driver).LoginAsUser(_baseUrl, _loginEmail, _password);

            new MainPage(_driver).OpenTopEmail();

            // Assert
            new MainPage(_driver).OpendEmailSubject.Text.Should().Contain(Constants.TestEmailSubject);

            // Act
            new MainPage(_driver).LogOut();

            // Assert
            new LoginPage(_driver).LoginButton.Displayed.Should().BeTrue();
        }
    }
}


