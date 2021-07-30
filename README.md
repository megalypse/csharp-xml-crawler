# XmlCrawler
This life purpose of this package is to simplify even more your life with XML files in C#. Let me show you how to use it.
Let's assume you have a file called `random_xml.xml` in the same directory as the exemplified `program.cs` used here, with the following content:
```xml
<?xml version="1.0"?>
<catalog>
   <book id="bk101">
      <author>Gambardella, Matthew</author>
      <title>XML Developer's Guide</title>
      <genre>Computer</genre>
      <price>44.95</price>
      <publish_date>2000-10-01</publish_date>
      <description>An in-depth look at creating applications 
      with XML.</description>
   </book>
</catalog>
```
In your `program.cs`, the first thing you need to do is to extract the xml string from our hipothetical file:
```csharp
string xmlString = File.ReadAllText("./random_xml.xml");
```
Then you simply instantiates a new object of the `XmlCrawler` class passing the `xmlString` variable as an argument:
```csharp
string xmlString = File.ReadAllText("./random_xml.xml");
XmlCrawler crawler = new(xmlString);
```
Or if for some reason, you already have an instance of `XDocument`, you can create an instance with it too:
```csharp
string xmlString = File.ReadAllText("./random_xml.xml");
XDocument document = XDocument.Parse(xmlString);
XmlCrawler crawler = new(document);
```
> You can check the current node in the crawler by checking the `CurrentNode` property.

From there, you can keep walking through the XML tags by using the `FindNode` method. Let's say we want to get to the book tag:
```csharp
crawler.FindNode("book");
Console.WriteLine(crawler.CurrentNode.Name.LocalName); // book
```


Now, let's say we want to get the author's name from the book tag we just crawled in:
```csharp
crawler.FindNode("book");
Console.WriteLine(crawler.GetChildValue("author")); // Gambardella, Matthew
```
> **Note**: We need to crawl through all the parent elements first the get to the one we want. If we tried to use the `FindNode` method or the `GetChildValue` method while inside the `catalog` tag, the crawler would throw a `NodeNotFoundException`.

If we want to get back to the root element, we can simply use the `Reset` method:
```csharp
crawler.FindNode("book");
Console.WriteLine(crawler.CurrentNode.Name.LocalName); // book

crawler.Reset();
Console.WriteLine(crawler.CurrentNode.Name.LocalName); // catalog
```
> See? Although we made the crawler creep to the book element just like we wanted, after we reseted, it got back to the first element from the XML file.

Thanks for downloading the package, or simply reading through the whole documentation. I hope I manage to help you and if you have any suggestion, commentary or bug report, please, feel free to contact me.

> Written with [StackEdit](https://stackedit.io/).
