using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace ConsoleApplication2
{
    public class BookModel
    {
        public BookModel()
        {

        }

        private string bookType;
        public string BookType
        {
            get { return bookType; }
            set { bookType = value; }
        }

        private string bookISBN;
        public string BookISBN
        {
            get { return bookISBN; }
            set { bookISBN = value; }
        }

        private string bookName;
        public string BookName
        {
            get { return bookName; }
            set { bookName = value; }
        }

        private string bookAuthor;
        public string BookAuthor
        {
            get { return bookAuthor; }
            set { bookAuthor = value; }
        }

        private double bookPrice;
        
        public double BookPrice
        {
            set { bookPrice = value; }
            get { return bookPrice; }
        }
       
    }


    class Program
    {
        static void Main(string[] args)
        {
            string infile = @"D:\demo\xmlParseDemo.xml";
            string outfile = @"D:\demo\watch.json";
            ParseXML(infile, outfile);
        }

        public static void ParseXML(string infile, string outfile)
        {
            List<BookModel> bookModelList = new List<BookModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(infile);
            XmlElement root = doc.DocumentElement;

            XmlNodeList listNodes = root.SelectNodes(string.Format("/bookstore/book[title=\"{0}\"][price={1}]", "Harry", "39.95"));
            foreach(XmlNode xn in listNodes)
            {
                Console.WriteLine(xn.InnerText);
            }
            Console.ReadKey();

            /*
           XmlNode xn = doc.SelectSingleNode("bookstore");
           XmlNodeList xnl = xn.ChildNodes;
           foreach(XmlNode xn1 in xnl)
           {
               BookModel bookModel = new BookModel();
               XmlElement xe = (XmlElement)xn1;

               bookModel.BookType = xe.GetAttribute("category").ToString();

               XmlNodeList xn10 = xe.ChildNodes;
               bookModel.BookAuthor = xn10.Item(1).InnerText;
               bookModel.BookPrice = Convert.ToDouble(xn10.Item(2).InnerText);
               Console.WriteLine("{0}\t{1}", bookModel.BookAuthor, bookModel.BookPrice);
               bookModelList.Add(bookModel);
           }
           */

            /*
            XPathNavigator nav = document.CreateNavigator();
            XPathNodeIterator queryNodeIterator = nav.Select("/PropertyBag/s_AnswerResponseCommand/s_AnswerQueryResponse/a_AnswerDataArray/s_AnswerData");
            while(queryNodeIterator.MoveNext())
            {
                XPathNavigator queryNav = queryNodeIterator.Current;
                string data = queryNav.SelectSingleNode("c_AnswerServiceName").Value;
                Console.WriteLine(data);
            }
            Console.ReadKey();
            */
        }
    }
}
