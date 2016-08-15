using Machine.Specifications;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.ObjectModel;

namespace DocumentCentreTests
{
    public class MyWebDriver : IWebDriver
    {
        private IWebDriver _driver;
        internal string _currentTest { get; set; }

        internal MyWebDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Url
        {
            get
            {
                return _driver.Url;
            }

            set
            {
                _driver.Url = value;
            }
        }

        public string Title
        {
            get
            {
                return _driver.Title;
            }
        }

        public string PageSource
        {
            get
            {
                return _driver.PageSource;
            }
        }

        public string CurrentWindowHandle
        {
            get
            {
                return _driver.CurrentWindowHandle;
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return _driver.WindowHandles;
            }
        }

        public void Close()
        {
            _driver.Close();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
