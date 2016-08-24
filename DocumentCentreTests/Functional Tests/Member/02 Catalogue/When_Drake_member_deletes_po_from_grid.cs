using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_Drake_member_deletes_po_from_grid : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static MemberHomePage _memHomePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Order Grid Delete Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _memHomePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
            _voPage = _memHomePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
            _voPage.OrderType = Constants.OrderStatus.DRAFT;
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };

        Because of = () =>
        {
            try
            {
                _voPage.DeleteOrder();
            }
            catch (System.Exception)
            {
                _logger.Fatal("-- Drake Order Grid Delete Test: [FAILED] --");
            }
        };

        It should_delete_the_order = () =>
        {
            if (_memHomePage.PageReached)
            {
                _logger.Info("-- Drake Order Grid Delete Test: [PASSED] --");
                _memHomePage.PageReached.ShouldBeTrue(); ;
            }
            else
            {
                _logger.Fatal("-- Drake Order Grid Delete Test: [FAILED] --");
                _memHomePage.PageReached.ShouldBeTrue();
            }
        };
    }
}
