using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ParseImages
{
    class Program
    {
        static List<string> links = new List<string>();
        static List<string> linksToImages = new List<string>();
        static void Main(string[] args)
        {
            SaveImages();
            Console.Beep();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void SaveImages()
        {
            Console.WriteLine("Введите имя папки: ");
            string folderName = Console.ReadLine();
            string path = $@"c:\Users\Ezerath\Desktop\images\{folderName}";
            CreateFolder(path);
            WebClient client = new WebClient();
            List<Image> sheets = GetSmartImages();
            foreach (var item in sheets)
            {
                client.DownloadFile(item.Url, $@"{path}\{item.Name}.jpg");
            }
        }
        private static List<Image> GetSmartImages()
        {
            var list = new List<Image>();
            string baseUrl = "http://dsplit.ru";
            for (int page = 1; page <= 5; page++)
            {
                string url = baseUrl + "/products/plitnye_materialy/plity_tss_smart_pod_zakaz/?PAGEN_1=" + page;
                string request = "//div[@class='item_compare_wrapper']//a";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var result = doc.DocumentNode.SelectNodes(request);
                foreach (var item in result)
                {
                    string _url = baseUrl + item.GetAttributeValue("href", null);
                    string imageRequest = "//div[@class='big-photo']//a";
                    doc = web.Load(_url);
                    var imageLink = doc.DocumentNode.SelectSingleNode(imageRequest);
                    string name = imageLink.GetAttributeValue("data-title", null).Replace('*', 'x');
                    string imageUrl = baseUrl + imageLink.GetAttributeValue("href", null);
                    list.Add(new Image(name, imageUrl));
                }
            }
            return list;
        }
        private static List<Image> GetSheetsImagesRusLam()
        {
            List<Image> list = new List<Image>();
            string baseUrl = "https://www.ruslaminat.ru";
            string url = baseUrl + "/catalog/dekory/";
            string request = "//li[@class='b-decors__item']//a[@class='b-decors__link']";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var result = doc.DocumentNode.SelectNodes(request);
            foreach (var item in result)
            {
                string _url = baseUrl + item.GetAttributeValue("href", null);
                string _request = "//div[@class='b-item-pic']//a";
                doc = web.Load(_url);
                string link = baseUrl + doc.DocumentNode.SelectSingleNode(_request).GetAttributeValue("href", null);
                string name = item.GetAttributeValue("title", null);
                list.Add(new Image(name, link));
            }
            return list;
        }
        private static List<Image> GetKitchenTopImagesKedr()
        {
            var list = new List<Image>();
            string baseUrl = "http://kedrcompany.ru";
            for (int page = 1; page <= 10; page++)
            {
                string url = baseUrl + "/decors/?PAGEN_1=" + page;
                string request = "//div[@class='item']//a[@target='_blank']";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var result = doc.DocumentNode.SelectNodes(request);
                foreach (var item in result)
                {
                    string link = baseUrl + item.GetAttributeValue("href", null);
                    doc = web.Load(link);
                    string _request = "//div[@class='detail-img']//a[@class='fancybox detail-img-show']";
                    string _url = baseUrl + doc.DocumentNode.SelectSingleNode(_request).GetAttributeValue("href", null);
                    string name = doc.DocumentNode.SelectSingleNode("//div[@class='fl']//h1").InnerText.Replace('/', '-').Replace('*', '-');
                    list.Add(new Image(name, _url));
                }
            }
            return list;
        }
        private static List<Image> GetKitchenTopImagesAlphaLux()
        {
            var list = new List<Image>();
            string baseUrl = "http://www.topstarpostforming.com";
            for (int page = 1; page <= 1; page++)
            {
                string url = baseUrl + "/decors/patterns-16.html";
                string request = "//div[@class='glhide']//img";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var result = doc.DocumentNode.SelectNodes(request);
                foreach (var item in result)
                {
                    string _url = baseUrl + item.GetAttributeValue("src", null);
                    string name = item.GetAttributeValue("alt", null);
                    list.Add(new Image(name, _url));
                }
            }
            return list;
        }
        private static List<Image> GetKitchenTopImagesSistec()
        {
            string baseUrl = "https://sistec.ru";
            var list = new List<Image>();
            for (int page = 1; page <= 2; page++)
            {
                string url = baseUrl + "/catalog/stoleshnitsy/?PAGEN_1=" + page;
                string request = "//div[@class='item-wrapp']//a";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var result = doc.DocumentNode.SelectNodes(request);
                foreach (var item in result)
                {
                    try
                    {
                        string link = baseUrl + item.GetAttributeValue("href", null);
                        string _request = "//div[@class='cti-photo']//img";
                        doc = web.Load(link);
                        string _link = baseUrl + doc.DocumentNode.SelectSingleNode(_request).GetAttributeValue("src", null);
                        string name = doc.DocumentNode.SelectSingleNode(_request).GetAttributeValue("alt", null);
                        list.Add(new Image(name, _link));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        continue;
                    }
                }
            }
            return list;
        }
        private static List<Image> GetKitchenTopImagesMakmart()//Makmart
        {
            int page = 0;
            string baseUrl = "https://makmart.ru";
            var list = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                string url = baseUrl + "/r9/?Page=" + page;
                string request = "//div[@class='bc-img']//a";
                HtmlWeb web = new HtmlWeb();
                //HtmlNode.ElementsFlags["option"] = 5;
                HtmlDocument doc = web.Load(url);
                var result = doc.DocumentNode.SelectNodes(request);
                foreach (var item in result)
                {
                    string link = baseUrl + item.GetAttributeValue("href", null);
                    string _request = "//div[@class='bigImg']//a";
                    doc = web.Load(link);
                    string _link = baseUrl + doc.DocumentNode.SelectSingleNode(_request).GetAttributeValue("href", null);
                    string name = doc.DocumentNode.SelectSingleNode("//div[@class='featuresBlock']//h3").InnerText;
                    list.Add(new Image(name, _link));
                }
                page++;
            }
            return list;
        }

        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        static List<Image> GetAlvicImages()
        {
            List<Image> images = new List<Image>();
            string url = "https://www.grupoalvic.com/en/tr-max-euroforming/";
            string request = "//div[@class='item-familia']//a";
            // если не удается получить доступ к сайту с https
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var result = doc.DocumentNode.SelectNodes(request);
            foreach (var item in result)
            {
                string linkUrl = item.GetAttributeValue("href", null);
                doc = web.Load(linkUrl);
                var urls = doc.DocumentNode.SelectSingleNode("//div[@class='muestra-producto zoom']//img");
                string link = urls.GetAttributeValue("src", null);
                string name = urls.GetAttributeValue("title", null).Replace('/', ' ');
                //doc.DocumentNode.SelectNodes(request);
                //foreach (var l in urls)
                //{
                //    string _url = l.GetAttributeValue("href", null);
                //    doc = web.Load(_url);
                //    var _urls = doc.DocumentNode.SelectSingleNode("//div[@class='muestra-producto zoom']//img");
                //    string link = _urls.GetAttributeValue("src", null);
                //    string name = _urls.GetAttributeValue("title", null);
                //    images.Add(new Image(name, link));
                //}
                images.Add(new Image(name, link));
            }
            return images;
        }
    }
}
