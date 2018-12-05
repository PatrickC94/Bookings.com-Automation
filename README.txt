Prerequisites required to run the tests:
1. A chrome driver installed. This can be done by using "npm install chromedriver" and take note of the location of the driver.
2. Open the BookingsProject.cs in Visual Studio.
3. Download and install the NuGet packages for Selenium.Webdriver, NUnit and NUnit3Adapter.
4. Update: driver = new ChromeDriver(@"Location of chromedriver");
5. Update: System.IO.File.WriteAllLines("Location to output results", results);

Now you can run the tests.