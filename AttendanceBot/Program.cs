using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

string email = Environment.GetEnvironmentVariable("HONO_USERNAME") ?? "595";
string password = Environment.GetEnvironmentVariable("HONO_PASSWORD") ?? "YHRR#spain#2028";

var options = new ChromeOptions();
options.AddArgument("--headless");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");

using var driver = new ChromeDriver(options);

try
{
    driver.Navigate().GoToUrl("https://nirvana.hono.ai/login");

    driver.FindElement(By.Id("username_id")).SendKeys(email);
    driver.FindElement(By.Id("password")).SendKeys(password);
    driver.FindElement(By.Id("loginbutton")).Click();

    Thread.Sleep(7000); // Wait for dashboard

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
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    driver.Quit();
}
