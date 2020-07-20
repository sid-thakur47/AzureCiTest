using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureCiTest
{
    public class AzureTest
    {
        IWebDriver driver = new FirefoxDriver();

        [Test]
        public void TestNews()
        {
            try
            {
                driver.Url = "https://news.ycombinator.com/";
                string actual = driver.Url;
                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);
                IList<IWebElement> header = driver.FindElements(By.ClassName("storylink"));
                List<string> mylist = new List<string>();
                foreach (var items in header)
                {
                    string text = items.Text;
                    mylist.Add(text);
                    Console.WriteLine(text);
                }

                var mostRepeatedWord = mylist.SelectMany(x => x.Split(new[] { " " },
                                                 StringSplitOptions.RemoveEmptyEntries))
                                 .GroupBy(x => x).OrderByDescending(x => x.Count())
                                 .Select(x => x.Key).FirstOrDefault();
                Console.WriteLine("most repeated word" + "---->" + mostRepeatedWord);


                IList<IWebElement> point = driver.FindElements(By.ClassName("score"));
                List<int> pointlist = new List<int>();
                foreach (var items in point)
                {

                    string text = items.Text;
                    string filteredText = text.Replace("points", " ");
                    pointlist.Add(int.Parse(filteredText));
                    Console.WriteLine(filteredText);
                }

                Dictionary<string, int> dict = new Dictionary<string, int>();
                for (int i = 0; i < mylist.Count; i++)
                {
                    dict.Add(mylist[i], pointlist[i]);
                }

                foreach (var code in dict)
                {
                    Console.WriteLine(code);

                }
                Console.WriteLine("max points and headlines" + "-->" + dict.Keys.Max() + "----->" + dict.Values.Max());
                string expectedUrl = ("https://news.ycombinator.com/");
                Assert.AreEqual(expectedUrl, actual);
                Thread.Sleep(3000);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
