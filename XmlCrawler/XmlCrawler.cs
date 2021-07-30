using System;
using System.Linq;
using System.Xml.Linq;
using CrawlerXml.Exceptions;

namespace CrawlerXml
{
    public class XmlCrawler
    {
        public XmlCrawler(string xmlString)
        {
            RootNode = XDocument.Parse(xmlString.Trim()).Root;
            CurrentNode = RootNode;
        }

        public XmlCrawler(XDocument document)
        {
            RootNode = document.Root;
            CurrentNode = RootNode;
        }

        public XElement CurrentNode { get; internal set; }
        public string Value { get => CurrentNode.Value; }
        private XElement RootNode { get; }

        public string GetChildValue(string childName) => Find(childName)?.Value;

        public XmlCrawler FindNode(string nodeName)
        {
            var searchResult = Find(nodeName);

            if (searchResult is null)
                return null;

            CurrentNode = searchResult;
            return this;
        }

        public XmlCrawler FindNodeOrFail(string nodeName)
        {
            CurrentNode = Find(nodeName) ?? throw new NodeNotFoundException(nodeName);
            return this;
        }

        public void Reset()
        {
            CurrentNode = RootNode;
        }

        private XElement Find(string nodeName)
        {
            return CurrentNode
              .Elements()
              .ToList()
              .Where(x => x.Name.LocalName == nodeName)
              .SingleOrDefault();
        }
    }
}
