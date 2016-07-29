﻿using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Purchase_Order_Tests
{
    public class When_Drake_member_makes_po : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _cataPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Member PO Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.DRAKE_MEMBER_USER, Constants.DRAKE_MEMBER_PASS);
            _cataPage = _homePage.NavigateToCatalogues();
            _cataPage.InputCatalogueName("Knauf Insulation");
            _cataPage.InitiateSearch();
            _cartPage = _cataPage.ChooseDrakeCatalogue("Knauf Insulation");
        };

        Because of = () =>
        {
            _cartPage.AddItemInline("EE100");
        };

        It should_complete_purchase_order = () =>
        {
            if (_cartPage.AlertSuccess.Equals(true))
            {
                _logger.Fatal("-- Drake Member PO Test: [PASSED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Fatal("-- Drake Member PO Test: [FAILED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
        };
    }
}
