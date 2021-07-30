using CrawlerXml;
using CrawlerXml.Exceptions;
using System;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace Tests
{
    public class XmlCrawlerTest
    {
        [Fact]
        public void InstantiatesCrawlerThroughXmlString()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            Assert.Equal("catalog", crawler.CurrentNode.Name.LocalName);
        }

        [Fact]
        public void InstantiatesCrawlerThroudXDocument()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            var document = XDocument.Parse(xmlString);
            XmlCrawler crawler = new(document);

            Assert.Equal("catalog", crawler.CurrentNode.Name.LocalName);
        }

        [Fact]
        public void GetTheValueFromTheSelectedTag()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            Assert.Equal("Gambardella, Matthew", crawler.FindNodeOrFail("book").GetChildValue("author"));
        }

        [Fact]
        public void ReturnsNullWhenChildDoesNotExist()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            Assert.Null(crawler.FindNodeOrFail("book").GetChildValue("authore"));
        }

        [Fact]
        public void ResetsTheCrawlerToTheRootElement()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            crawler.FindNodeOrFail("book");
            crawler.Reset();

            Assert.Equal("catalog", crawler.CurrentNode.Name.LocalName);
        }

        [Fact]
        public void ThrowsWhenNodeNotFound()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            crawler.FindNodeOrFail("book");

            Assert.Throws<NodeNotFoundException>(() => crawler.FindNodeOrFail("oudri kanda larrai"));
        }

        [Fact]
        public void ReturnsNullWhenNodeNotFound()
        {
            string xmlString = File.ReadAllText("./random_xml.xml");
            XmlCrawler crawler = new(xmlString);

            var result = crawler.FindNode("unexistent node");

            Assert.Null(result);
            Assert.Equal("catalog", crawler.CurrentNode.Name.LocalName);
        }
    }
}
