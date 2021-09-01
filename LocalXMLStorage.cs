using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;

namespace PhotoLibraryApp
{
    public class LocalXMLStorage
    {
        private string fileName = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Gallery.xml");

        public bool newLineOnAttributes { get; set; } = true;

        public LocalXMLStorage()
        {

        }

        public LocalXMLStorage(string filePath) : this()
        {
            this.fileName = filePath;
        }

        public LocalXMLStorage(string filePath, bool newLineOnAttributes) : this()
        {
            this.fileName = filePath;
            this.newLineOnAttributes = newLineOnAttributes;
        }

        /// <summary>
        /// Serializes an object to an XML string.
        /// </summary>
        public static string ToXml(object obj)
        {
            var ns = new XmlSerializerNamespaces();
            var newLineOnAttributes = true;
            ns.Add("", "");
            return ToXml(obj, ns, newLineOnAttributes);
        }

        /// <summary>
        /// Serializes an object to an XML string, using the specified namespaces.
        /// </summary>
        public static string ToXml(object obj, XmlSerializerNamespaces ns, bool NewLineOnAttributes)
        {
            Type T = obj.GetType();

            var xs = new XmlSerializer(T);
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true };

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, ws))
            {
                xs.Serialize(writer, obj, ns);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Deserializes an object from an XML string.
        /// </summary>
        public static T ReadXml<T>(string xml) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML string, using the specified type name.
        /// </summary>
        public static object ReadXml(string xml, string typeName)
        {
            Type T = Type.GetType(typeName);
            XmlSerializer xs = new XmlSerializer(T);
            using (StringReader sr = new StringReader(xml))
            {
                return xs.Deserialize(sr);
            }
        }

        //public abstract void AppendToXmlFile(Object obj);

        public void writeXmlFile(Object obj)
        {
            var xs = new XmlSerializer(obj.GetType());
            var ns = new XmlSerializerNamespaces();
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = newLineOnAttributes, OmitXmlDeclaration = true };
            ns.Add("", "");

            using (XmlWriter writer = XmlWriter.Create(this.fileName, ws))
            {
                xs.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Serializes an object to an XML file.
        /// </summary>
        public static void WriteXmlFile(Object obj, string filePath, bool NewLineOnAttributes)
        {
            var xs = new XmlSerializer(obj.GetType());
            var ns = new XmlSerializerNamespaces();
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true };
            ns.Add("", "");

            using (XmlWriter writer = XmlWriter.Create(filePath, ws))
            {
                xs.Serialize(writer, obj);
            }
        }

        public T readXmlFile<T>() where T : class
        {
            StreamReader sr = new StreamReader(this.fileName);
            try
            {
                var result = ReadXml<T>(sr.ReadToEnd());
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("There was an error attempting to read the file " + this.fileName + "\n\n" + e.InnerException.Message);
            }
            finally
            {
                sr.Close();
            }
        }

        /// <summary>
        /// Deserializes an object from an XML file.
        /// </summary>
        public static T ReadXmlFile<T>(string filePath) where T : class
        {
            StreamReader sr = new StreamReader(filePath);
            try
            {
                var result = ReadXml<T>(sr.ReadToEnd());
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("There was an error attempting to read the file " + filePath + "\n\n" + e.InnerException.Message);
            }
            finally
            {
                sr.Close();
            }
        }
    }
}
