using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WPEx
{
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }

    public class A
    {
        [XmlAttribute]
        public int i = 10;
        public B b = new B() { i = 10 };
        public C c = new C() { i = 90 };
    }

    public class B
    {
        [XmlAttribute]
        public int i;
        public D d = D.getIstance();
    }

    public class C
    {
        [XmlAttribute]
        public int i;
        public D d = D.getIstance();
    }

    public class D
    {
        private static D _ist;
        private D()
        { }

        public static D getIstance()
        {
            if (_ist == null)
            {
                _ist = new D();
                _ist.i = 100;
            }
            return _ist;
        }
        [XmlAttribute]
        public int i;
    }

    public class MioStato
    {
        public int a = 90;
        public List<int> l = new List<int>();
        public SerializableDictionary<int, string> dic = new SerializableDictionary<int, string>();
        public MioStato()
        {
            l.Add(10);
            l.Add(15);

            dic.Add(0, "abao");
            dic.Add(1, "abaco");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pf = @"C:\Users\Bobaldo\Desktop\";
            var oa = new A();
            var ob = new B();
            var oc = new C();
            var xas = new XmlSerializer(typeof(A));
            //var xbs = new XmlSerializer(typeof(B));
            //var xcs = new XmlSerializer(typeof(C));
            //var xds = new XmlSerializer(typeof(D));
            if (File.Exists(pf + "a.xml"))
                File.Delete(pf + "a.xml");
            using (var xf = new FileStream(pf + "a.xml", FileMode.CreateNew))
            {
                xas.Serialize(xf, oa);
                xf.Flush();
            }

            using (var xf = new FileStream(pf + "a.xml", FileMode.Open))
            {
                var ood = xas.Deserialize(xf) as A;
            }

            //using (var xf = new FileStream(pf + "b.xml", FileMode.CreateNew))
            //{
            //    xas.Serialize(xf, ob);
            //    xf.Flush();
            //}

            //using (var xf = new FileStream(pf + "c.xml", FileMode.CreateNew))
            //{
            //    xas.Serialize(xf, oc);
            //    xf.Flush();
            //}

            //using (var xf = new FileStream(pf + "d.xml", FileMode.CreateNew))
            //{
            //    xas.Serialize(xf, oc);
            //    xf.Flush();
            //}

            //second element
            var o = new MioStato();
            var nf = "ts.xml";
            var xs = new XmlSerializer(typeof(MioStato));
            if (File.Exists(pf + nf))
                File.Delete(pf + nf);
            using (var xf = new FileStream(pf + nf, FileMode.Create))
            {
                xs.Serialize(xf, o);
                xf.Flush();
                Console.ReadLine();
            }
            using (var xf = new FileStream(pf + nf, FileMode.Open))
            {
                var ood = xs.Deserialize(xf) as MioStato;
            }
            Console.ReadLine();
        }
    }
}