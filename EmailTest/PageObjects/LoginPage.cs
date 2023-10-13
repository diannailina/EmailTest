using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace EmailTest.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
        
        [FindsBy(How = How.XPath, Using = "//*[@id=\"login-view\"]/div[2]/div/div[1]/div[1]/form/div[1]/div/div/div/div/div/input")]
        public IWebElement EmailField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"login-view\"]/div[2]/div/div[1]/div[1]/form/div[2]/div/div/div/div/div/input")]
        public IWebElement PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"login-view\"]/div[2]/div/div[1]/div[1]/form/div[4]/button/div")]
        public IWebElement LoginButton { get; set; }

        public void LoginAsUser(string baseUrl, string login, string password)
        {
            _driver.Navigate().GoToUrl(baseUrl);

            EmailField.Clear();
            EmailField.SendKeys(login);

            PasswordField.Clear();
            PasswordField.SendKeys(password);

            LoginButton.Click();
        }
    }
}