#if SELENIUM
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace DisasterAlleviationApp.Tests
{
    [TestClass]
    public class SeleniumUITests
    {
        private IWebDriver? _driver;
        private readonly string _baseUrl = Environment.GetEnvironmentVariable("DISASTER_APP_URL") ?? "http://localhost:5000";

        [TestInitialize]
        public void TestInitialize()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var options = new ChromeOptions();

            // headless toggle via environment variable (default = true)
            var headlessEnv = Environment.GetEnvironmentVariable("SELENIUM_HEADLESS") ?? "true";
            var headless = headlessEnv.Equals("true", StringComparison.OrdinalIgnoreCase);

            if (headless)
            {
                // Headless by default for CI. Set SELENIUM_HEADLESS=false to see the browser locally.
                options.AddArgument("--headless=new");
            }

            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            _driver = new ChromeDriver(service, options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            try { _driver?.Quit(); } catch { }
            try { _driver?.Dispose(); } catch { }
        }

        [TestMethod]
        public void CreateDonation_ValidInput_RedirectsToIndex()
        {
            // Arrange
            var driver = _driver ?? throw new AssertInconclusiveException("WebDriver not initialized");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Act
            driver.Navigate().GoToUrl(new Uri(new Uri(_baseUrl), "/Donations/Create"));

            var selectElem = wait.Until(d => d.FindElement(By.Name("Category")));
            var select = new OpenQA.Selenium.Support.UI.SelectElement(selectElem);
            select.SelectByText("Food");

            var qty = driver.FindElement(By.Name("Quantity"));
            qty.Clear();
            qty.SendKeys("5");

            var submit = driver.FindElements(By.CssSelector("button[type='submit'], button.btn-primary")).FirstOrDefault();
            Assert.IsNotNull(submit, "Submit button not found");
            submit!.Click();

            // Assert
            wait.Until(d => d.FindElements(By.XPath("//h2[contains(., 'Donations') or contains(., 'Donations')] ")).Any());
            Assert.IsTrue(driver.Url.Contains("/Donations"), "Expected to be on Donations index page after submit");
        }

        [TestMethod]
        public void CreateDonation_InvalidInput_ShowsValidationError()
        {
            // Arrange
            var driver = _driver ?? throw new AssertInconclusiveException("WebDriver not initialized");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Act
            driver.Navigate().GoToUrl(new Uri(new Uri(_baseUrl), "/Donations/Create"));

            // Disable client-side validation and submit empty category + zero quantity
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.forms[0].setAttribute('novalidate','novalidate');");
            js.ExecuteScript("var c = document.getElementsByName('Category')[0]; if(c) c.value=''; var q = document.getElementsByName('Quantity')[0]; if(q) q.value='0';");
            js.ExecuteScript("document.forms[0].submit();");

            // Assert - validation message expected
            var msg = wait.Until(d => d.FindElements(By.CssSelector("span[data-valmsg-for='Category'], span.field-validation-error")).FirstOrDefault());
            Assert.IsNotNull(msg, "Expected validation message element for Category");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(msg!.Text), "Expected non-empty validation text for Category");
            Assert.IsTrue(driver.Url.Contains("/Donations/Create"), "Expected to remain on the Create page when submission is invalid");
        }

        [TestMethod]
        public void Login_ValidCredentials_RedirectsToDashboard()
        {
            // Arrange
            var driver = _driver ?? throw new AssertInconclusiveException("WebDriver not initialized");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Act
            driver.Navigate().GoToUrl(new Uri(new Uri(_baseUrl), "/Identity/Account/Login"));

            var email = wait.Until(d => d.FindElement(By.CssSelector("input[type='email'], input[name='Input.Email'], input[id$='Email']")));
            var password = driver.FindElement(By.CssSelector("input[type='password'], input[name='Input.Password'], input[id$='Password']"));

            // Use seeded admin credentials (ensure these exist in your DB)
            email.Clear();
            email.SendKeys("admin@disaster.com");
            password.Clear();
            password.SendKeys("Admin#1234");

            var submit = driver.FindElements(By.CssSelector("button[type='submit'], button.btn-primary")).FirstOrDefault();
            Assert.IsNotNull(submit, "Login submit button not found");
            submit!.Click();

            // Assert - presence of logout link indicates successful login
            wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Log out') or contains(., 'Logout') or contains(., 'Sign out') or contains(., 'Log off')]")).Any());
            var logout = driver.FindElements(By.XPath("//a[contains(., 'Log out') or contains(., 'Logout') or contains(., 'Sign out') or contains(., 'Log off')]")).FirstOrDefault();
            Assert.IsNotNull(logout, "Expected a logout link after successful login");
        }

        [TestMethod]
        public void Login_InvalidCredentials_ShowsErrorMessage()
        {
            // Arrange
            var driver = _driver ?? throw new AssertInconclusiveException("WebDriver not initialized");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Act
            driver.Navigate().GoToUrl(new Uri(new Uri(_baseUrl), "/Identity/Account/Login"));

            var email = wait.Until(d => d.FindElement(By.CssSelector("input[type='email'], input[name='Input.Email'], input[id$='Email']")));
            var password = driver.FindElement(By.CssSelector("input[type='password'], input[name='Input.Password'], input[id$='Password']"));

            email.Clear();
            email.SendKeys("nonexistent@example.com");
            password.Clear();
            password.SendKeys("WrongPassword1!");

            var submit = driver.FindElements(By.CssSelector("button[type='submit'], button.btn-primary")).FirstOrDefault();
            Assert.IsNotNull(submit, "Login submit button not found");
            submit!.Click();

            // Assert - validation summary or other error container should appear
            var summary = wait.Until(d => d.FindElements(By.CssSelector("div.validation-summary-errors, div.text-danger, div.alert-danger")).FirstOrDefault());
            Assert.IsNotNull(summary, "Expected an error summary element after invalid login");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(summary!.Text), "Expected non-empty error message on invalid login");
        }
    }
}
#endif
