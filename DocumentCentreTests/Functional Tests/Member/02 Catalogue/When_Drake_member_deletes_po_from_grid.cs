using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_Drake_member_deletes_po_from_grid : BaseDriverTest
    {
        private static ViewOrdersPage _voPage;
        private static MemberHomePage _memHomePage;

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
                if (_voPage.AlertSuccess)
                    _logger.Info("-- Drake Order Grid Delete Test: [PASSED] --");
                else
                {
                    _logger.Fatal("-- Drake Order Grid Delete Test: [FAILED] --");
                    _voPage.AlertSuccess.ShouldBeTrue();
                }
            }
            catch (System.Exception e)
            {
                _logger.Fatal("-- Drake Order Grid Delete Test: [EXCEPTION ENCOUNTERED] --");
                _logger.Info("-- Exception info: " + e.Message);
            }
        };

        It should_delete_the_order = () => _voPage.AlertSuccess.ShouldBeTrue();
    }
}
