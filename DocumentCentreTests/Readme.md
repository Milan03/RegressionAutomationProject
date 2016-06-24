# Document Centre Tests

Document Centre Tests is an automated testing project. The project utilizes the 
[Page Object Design Pattern](http://www.assertselenium.com/automation-design-practices/page-object-pattern/) 
along with the principles of Behaviour Driven Development (BDD) in the sense 
that a page is modelled on what it does and how a user interacts with it. As the test engine
Document Centre Tests uses [NCrunch](http://www.ncrunch.net/) which is an automated concurrent 
testing tool. This project aims to automate the regression testing of the Document Centre Member, 
Group, and Supplier Portals hosted by LBMX Incorporated.

### Prerequisities

For development both an instance of Visual Studio is required as well as the NCrunch Visual Studio
extension.

```
- Visual Studio 
- NCrunch v2.23
```

### Installing

Download and install the Visual Studio [extension](http://www.ncrunch.net/download). Once installed, 
open Visual Studio and enable NCrunch from the 'NCrunch' top bar menu. NCrunch will now recognize
any projects associated with it once they are open.

## Running the tests

Tests are run using the NCrunch interface while in Visual Studio. They can be set to run automatically
in the background while in development or manually by selecting individuals or sets. Tests can also
be offloaded to another computer/server with the use of NCrunch's Grid Node system.

## Authors

* **Milan Sobat** - *Initial work* 

