using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace QASConfig
{
    static class Helper
    {
        private static readonly XmlDictionaryReaderQuotas XmlDictionaryReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 512 * 1024 * 1024 };

        public static T CloneObject<T>(T obj)
        {
            using (Stream objectStream = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(objectStream, obj);
                objectStream.Flush();
                objectStream.Seek(0, SeekOrigin.Begin);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(objectStream, XmlDictionaryReaderQuotas);
                T objReturn = (T)ser.ReadObject(reader, true);
                reader.Close();
                return objReturn;
            }
        }

        public static void DataContractSerialize<T>(T obj, string fileName)
        {
            using (FileStream writer = new FileStream(fileName, FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(writer, obj);
            }
        }

        public static T DataContractDeSerialize<T>(string fileName)
        {
            T obj;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(T));

                    obj = (T)ser.ReadObject(reader, true);
                }
            }
            return obj;
        }

    }
}