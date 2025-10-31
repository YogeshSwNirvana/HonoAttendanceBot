using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

string email = Environment.GetEnvironmentVariable("HONO_USERNAME") ?? "595";
string password = Environment.GetEnvironmentVariable("HONO_PASSWORD") ?? "YHRR#spain#2028";

var options = new ChromeOptions();
options.AddArgument("--headless");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");

// Use explicit path for ChromeDriver from NuGet output directory
string? driverDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
var service = ChromeDriverService.CreateDefaultService(driverDirectory);

//Console.WriteLine("ChromeDriver Version: " + service.DriverVersion);

using var driver = new ChromeDriver(service, options);

try
{
    driver.Navigate().GoToUrl("https://nirvana.hono.ai/login");

    var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
    wait.Until(d => d.FindElement(By.Id("username_id")));

    driver.FindElement(By.Id("username_id")).SendKeys(email);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.Id("loginbutton")).Click();

    wait.Until(d => d.FindElement(By.XPath("//a[contains(text(), 'Mark-Out')]")));

    var markOutBtn = driver.FindElement(By.XPath("//a[contains(text(), 'Mark-Out')]"));
    if (markOutBtn != null)
    {
        Console.WriteLine("Logged In");
    }

    int hour = DateTime.UtcNow.AddHours(5.5).Hour;
    if (hour == 11)
    {
        driver.FindElement(By.XPath("//a[contains(text(), 'Mark-In')]")).Click();
        Console.WriteLine("Attendance Marked In.");
    }
    else if (hour == 21)
    {
        driver.FindElement(By.XPath("//a[contains(text(), 'Mark-Out')]")).Click();
        Console.WriteLine("Attendance Marked Out.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex}");
}
finally
{
    driver.Quit();
}

