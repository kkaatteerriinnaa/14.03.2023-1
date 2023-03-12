using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace ConsoleApp37
{
    [Serializable]
    [DataContract]
    public class Phone
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public Company Company { get; set; }

        public Phone(string name, int age, Company comp)
        {
            Name = name;
            Age = age;
            Company = comp;
        }
        
        public Phone()
        {

        }
    }
    [Serializable]
    [DataContract]
    public class Company
    {
        [DataMember]
        public string Name { get; set; }

        public Company() { }

        public Company(string name)
        {
            Name = name;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FileStream stream = null;
            XmlSerializer serializer = null;
            DataContractJsonSerializer jsonFormatter = null;
            Phone h = null;

            //Сеарилизация xml
            h = new Phone("Mi 9", 2023, new Company("Apple"));
            stream = new FileStream("../../data.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(Phone));
            serializer.Serialize(stream, h);
            stream.Close();
            Console.WriteLine("Сериализация успешно выполнена!");
            //Десиарилизация xml
            stream = new FileStream("../../data.xml", FileMode.Open);
            serializer = new XmlSerializer(typeof(Phone));
            h = (Phone)serializer.Deserialize(stream);
            Console.WriteLine(h.Name + "	" + h.Age + "	" + h.Company.Name);
            stream.Close();
            //Сеарилизация json
            h = new Phone("Mi 9", 2023, new Company("Apple"));
            stream = new FileStream("../../data.json", FileMode.Create);
            jsonFormatter = new DataContractJsonSerializer(typeof(Phone));
            jsonFormatter.WriteObject(stream, h);
            stream.Close();
            Console.WriteLine("Сериализация успешно выполнена!");
            //Десиарилизация json
            stream = new FileStream("../../data.json", FileMode.Open);
            jsonFormatter = new DataContractJsonSerializer(typeof(Phone));
            h = (Phone)jsonFormatter.ReadObject(stream);
            Console.WriteLine(h.Name + "	" + h.Age + "	" + h.Company.Name);
            stream.Close();
        }
    }
}
