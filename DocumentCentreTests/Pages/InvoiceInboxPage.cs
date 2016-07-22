using DocumentCentreTests.Models;
using DocumentCentreTests.Util;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class InvoiceInboxPage : CommonInboxPage
    {
        #region Web Elements
        //private IWebElement StatusDropdown;
        //private IWebElement StatusAll;
        //private IWebElement StatusUnprocessed;
        //private IWebElement StatusProcessed;
        //private IWebElement PeriodDropdown;

        private IWebElement ASStatus;
        private IWebElement ASPeriod;
        private IWebElement ASFrom;
        private IWebElement ASBillingType;
        private IWebElement ASShipTo;
        private IWebElement ASInvoiceNumber;
        private IWebElement ASPONumber;
        private IWebElement ASAmtFrom;
        private IWebElement ASAmtTo;
        private IWebElement ASDateAdded;
        private IWebElement ASInvoiceDate;
        private IWebElement ASNetDueDate;
        private IWebElement ASSearchBtn;
        private IWebElement ASClearBtn;
        private IWebElement ASBackToBasicBtn;

        //#region Period Years
        //protected IWebElement PeriodLast90;
        //protected IWebElement Period2017;
        //protected IWebElement Period2016;
        //protected IWebElement Period2015;
        //protected IWebElement Period2014;
        //protected IWebElement Period2013;
        //protected IWebElement Period2012;
        //protected IWebElement Period2011;
        //protected IWebElement Period2010;
        //protected IWebElement Period2009;
        //protected IWebElement Period2008;
        //#endregion

        //protected IList<IWebElement> _statusDropdowns;
        //protected IList<IWebElement> _periodDropdowns;
        //protected IList<IWebElement> _periodListCount;
        #endregion

        //internal bool PageReached;
        //internal bool AdvLoadSuccess;
        //internal bool BasicLoadSuccess;
        //internal bool GridLoadSuccess;
        //internal bool StatusDropdownSuccess;
        //internal bool StatusSetSuccess;
        //internal bool PeriodDropdownSuccess;
        //internal bool PeriodSetSuccess;
        public InvoiceInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;

        }
    
    }
}
