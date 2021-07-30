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

        public string GetChildValue(string childName) => Find(childName).Value;

        public XmlCrawler FindNode(string nodeName)
        {
            CurrentNode = Find(nodeName);
            return this;
        }

        public void Reset()
        {
            CurrentNode = RootNode;
        }

        private XElement Find(string nodeName)
        {
            try
            {
                return CurrentNode
                  .Elements()
                  .ToList()
                  .Where(x => x.Name.LocalName == nodeName)
                  .Single();
            }
            catch (Exception)
            {
                throw new NodeNotFoundException(nodeName);
            }
        }
    }
}
