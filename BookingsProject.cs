using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace BookingProject

{

    [TestFixture]
    public class Booking

    {

        // Declaring the web driver.
        IWebDriver driver;

        // Checks if an element is present on the page or not using a boolean.
        bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }

        //Creating a string array to store the results from the tests.
        string[] results = new string[20];

        //Setup only needs to be run once for all of the tests.
        [OneTimeSetUp]

        public void Initialize()
        {
            // Linking the driver with the chrome driver saved locally. (This needs to be updated with the location of own chromedriver).
            driver = new ChromeDriver(@"C:\Drivers\node_modules\chromedriver\lib\chromedriver");
            //Opens Chrome and maximises the screen size.
            driver.Manage().Window.Maximize();
            //Navigates to the specified Url: Booking.com
            driver.Url = "https://www.booking.com";

        }

        //Tests ordered to run concurrently.
        [Test, Order(1)]
        public void TypeInSearch()

        {
            // Test to locate the element for the search box and assign it to an object. 
            IWebElement SearchBox = driver.FindElement(By.XPath("//*[@id='ss']"));
            //Types a string into the search box.
            SearchBox.SendKeys("Limerick");
            //Wait 500ms before moving on to the next test.
            Task.Delay(500).Wait();

        }

        [Test, Order(2)]
        public void CheckIn()
        {
            // Locates the element for Checkin and clicks it.
            IWebElement CheckIn = driver.FindElement(By.ClassName("sb-date-field__icon-text"));

            CheckIn.Click();

            Task.Delay(1000).Wait();

        }


        [Test, Order(3)]
        public void MoveThreeMonths()

        {
            // Count used to specify the number of months in the future to move by.
            int count = 3;

            for (int i = 0; i < count; i++)

            {

                //If the next month button is present assign an object and click on it. Repeat 3 times for three months.
                //Note: The bookings.com site has two versions of this element which can change between when the page is reloaded thus if/else required.
                if (IsElementPresent(By.XPath("//*[@id='frm']/div[1]/div[2]/div[2]/div/div/div[2]")))
                {
                    IWebElement MoveThreeMonths = driver.FindElement(By.XPath("//*[@id='frm']/div[1]/div[2]/div[2]/div/div/div[2]"));

                    MoveThreeMonths.Click();

                    Task.Delay(1000).Wait();
                }

                else
                {
                    IWebElement MoveThreeMonths = driver.FindElement(By.ClassName("c2-button-further"));

                    MoveThreeMonths.Click();

                    Task.Delay(1000).Wait();
                }

            }
            Task.Delay(1000).Wait();
        }


        [Test, Order(4)]
        public void SelectDayIn()

        {

            //If the element for the 15th date is present proceed. (random date selected).
            //Note: Once again there is two versions that can appear and can change per reload thus if/else required.
            if (IsElementPresent(By.XPath("//table[.//th[contains(text(),'')]]//span[contains(text(),'15')]")))
            {
                IWebElement SelectDayIn = driver.FindElement(By.XPath("//table[.//th[contains(text(),'')]]//span[contains(text(),'15')]"));

                SelectDayIn.Click();

                Task.Delay(500).Wait();
            }

            else
            {
                IWebElement SelectDayIn = driver.FindElement(By.XPath("//table[.//tr[contains(text(),'')]]//td[contains(text(),'15')]"));

                SelectDayIn.Click();
            }


            Task.Delay(1000).Wait();
        }


        [Test, Order(5)]
        public void SelectDayOut()

        {

            //If the element for the 16th date is present proceed. (One night stay).
            //Note: Once again there is two versions that can appear and can change per reload thus if/else required.
            if (IsElementPresent(By.XPath("//table[.//th[contains(text(),'')]]//span[contains(text(),'16')]")))
            {
                IWebElement SelectDayOut = driver.FindElement(By.XPath("//table[.//th[contains(text(),'')]]//span[contains(text(),'16')]"));

                SelectDayOut.Click();

            }

            else
            {
                IWebElement SelectDayOut = driver.FindElement(By.XPath("//table[.//tr[contains(text(),'')]]//td[contains(text(),'16')]"));

                SelectDayOut.Click();
            }

            

            Task.Delay(500).Wait();

        }


        [Test, Order(6)]
        public void SelectSearchButton()

        {
            //Locate the element for the Search button and click on it.
            IWebElement Search = driver.FindElement(By.XPath("//*[@id='frm']/div[1]/div[4]/div[2]/button"));

            Search.Click();
            //Longer delay to ensure the page has loaded correctly.
            Task.Delay(3000).Wait();

        }


        [Test, Order(7)]
        public void FilterSauna()

        {

            //Locate the element for the Sauna filter button and clicks on it.
            IWebElement FilterSauna = driver.FindElement(By.XPath("//span[contains(text(),'Sauna')]"));

            FilterSauna.Click();

            Task.Delay(3000).Wait();
        }


        [Test, Order(8)]
        public void SaunaResults()

        {
            //Create the Headers for the results file to be outputted.
            results[0] = "Select Filter          Hotel Name           Is Listed";

            //Locates and identifies if the specified hotel names are present in the filtered result.
            //If they are present the they are added to the results as true otherwise false.
            if (IsElementPresent(By.XPath("//span[contains(text(),'Limerick Strand Hotel')]")))
            {

                results[1] = "Sauna          Limerick Strand Hotel          True";
            }

            else
            {
                results[1] = "Sauna          Limerick Strand Hotel          False";
            }

            if (IsElementPresent(By.XPath("//span[contains(text(),'George Limerick')]")))
            {
                results[2] = "Sauna          George Limerick          True";
            }

            else
            {

                results[2] = "Sauna          George Limerick          False";

            }

            Task.Delay(2000).Wait();
        }

        [Test, Order(9)]
        public void CloseSauna()
        {
            //Relocates the Sauna filter button and deselects it by clicking on the box.
            IWebElement CloseSauna = driver.FindElement(By.XPath("//*[@id='filter_popular_activities']/div[2]/a[1]"));

            CloseSauna.Click();

            Task.Delay(3000).Wait();

        }

        [Test, Order(10)]
        public void FilterFiveStar()

        {
            //Locate the element for the 5 Star filter button and clicks on it.
            IWebElement FiveStar = driver.FindElement(By.XPath("//span[contains(text(),'5 stars')]"));

            FiveStar.Click();

            Task.Delay(3000).Wait();

        }


        [Test, Order(11)]
        public void FiveStarResults()

        {
            //Locates and identifies if the specified hotel names are present in the filtered result.
            //If they are present they are added to the results as true otherwise false.
            if (IsElementPresent(By.XPath("//span[contains(text(),'The Savoy Hotel')]")))

            {
                results[3] = "5 Star          The Savoy Hotel          True";
            }

            else

            {
                results[3] = "5 Star          The Savoy Hotel          False";
            }

            if (IsElementPresent(By.XPath("//span[contains(text(),'George Limerick')]")))

            {
                results[4] = "5 Star          George Limerick          True";
            }

            else

            {
                results[4] = "5 Star          George Limerick          False";
            }

        }


        [Test, Order(12)]
        public void WriteResults()
        {
            //Writes the the contents of the results to a file in a specified location.
            //Note: This location will have to be changed to a location on each unique computer.
            System.IO.File.WriteAllLines(@"C:\Users\patrick.conway\Desktop\TestFolder\Results.txt", results);

        }

        //Teardown is only required to be carried out once the previous tests have completed.
        [OneTimeTearDown]
        public void Close()
        {
            //Close Chrome
            driver.Close();

        }

    }
}
