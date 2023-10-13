using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace EmailTest.PageObjects
{
    public class MainPage
    {
        private readonly IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail\"]/div/div[2]/div[1]/div/div/div[1]/button/div/div")]
        public IWebElement NewEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail-editor\"]/div[1]/div/div/div/div/div/div[1]/input")]
        public IWebElement ToFiled { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail-editor\"]/div[4]/div/div/div/div/div[1]/input")]
        public IWebElement SubjectField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail-editor\"]/div[6]/div/div")]
        public IWebElement ContentField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"modal\"]/div/div/div/div/div/div[1]/div/div[3]/button/div/div")]
        public IWebElement SendBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail\"]/div/div[2]/div[1]/div/drawer-menu/div/button[4]")]
        public IWebElement LogOutBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail\"]/div/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div/ul/li[1]/div")]
        public IWebElement TopEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"mail\"]/div/div[2]/div[3]/div/div[2]/div/div/div[2]/div/div/h4")]
        public IWebElement OpendEmailSubject { get; set; }

        public void SendAnEmail(string to) 
        {
            NewEmail.Click();

            ToFiled.Clear();
            foreach(char c in to.ToCharArray()) {
                ToFiled.SendKeys(c.ToString());
                Thread.Sleep(50);
            }

            SubjectField.Clear();
            SubjectField.SendKeys(Constants.TestEmailSubject);

            SendBtn.Click();
            // It is definitly not the best implemntation, bet because of the reason that the backend logic of the sselected email service may work slow, we need this sleep.
            Thread.Sleep(3000);
        }

        public void OpenTopEmail()
        {
            TopEmail.Click();
        }

        public void LogOut()
        {
            LogOutBtn.Click();
        }

    }
}
