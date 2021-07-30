using System;

namespace CrawlerXml.Exceptions
{
    [Serializable]
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException(string nodeName) : base($"Node \"{nodeName}\" does not exist.")
        { }

        public NodeNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected NodeNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
