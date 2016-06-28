using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Supply_America_Tests
{
    public class When_member_downloads_catalogue_pdf : BaseDriverTest
    {
        static MemberHomePage _memHomepage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Catalogue PDF Download Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _memHomepage = (MemberHomePage)loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
            _catPage = _memHomepage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue 02");
        };

        Because of = () => _prodPage.CataloguePDFExport();
       
        It should_download_the_catalogue_as_pdf = () =>
        {
            if (!_prodPage.PDFDownloaded)
            {
                _logger.Fatal("-- Member Catalogue PDF Download Test: [FAILED]");
                _prodPage.PDFDownloaded.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member Catalogue PDF Download Test: [PASSED]");
                _prodPage.PDFDownloaded.ShouldBeTrue();
            }
        };
    }
}
