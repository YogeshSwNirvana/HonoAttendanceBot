using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

//string email = Environment.GetEnvironmentVariable("HONO_USERNAME");
//string password = Environment.GetEnvironmentVariable("HONO_PASSWORD");

string email = "595";
string password = "YHRR#spain#2028";

var options = new ChromeOptions();
options.AddArgument("--headless");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");

using var driver = new ChromeDriver(options);

try
{
    driver.Navigate().GoToUrl("https://nirvana.hono.ai/login"); // Replace with actual

    driver.FindElement(By.Name("username")).SendKeys(email);
    driver.FindElement(By.Name("password")).SendKeys(password);
    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

    Thread.Sleep(5000); // Wait for dashboard

    int hour = DateTime.UtcNow.AddHours(5.5).Hour;
    if (hour == 11)
    {
        driver.FindElement(By.XPath("//*[text()='Mark In']")).Click();
        Console.WriteLine("✅ Attendance marked.");
    }
    else if (hour == 21)
    {
        driver.FindElement(By.XPath("//*[@id=\"navbar\"]/div/ul/li[1]/div/a")).Click();
        Console.WriteLine("Attendance Not marked.");
    }
        //Console.WriteLine("✅ Attendance marked.");
    }
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
}
finally
{
    driver.Quit();
}
