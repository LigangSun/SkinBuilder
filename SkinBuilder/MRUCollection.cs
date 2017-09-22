using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace ZLIS.SkinBuilder
{
    public class MRUCollection : CollectionBase
    {
        private static string mruFile = string.Empty;
        private int maxCount = 10;

        public void Add(string file)
        {
            if (this.Find(file))
            {
                this.MoveFront(file);
                return;
            }

            base.InnerList.Add(file);
            if (base.InnerList.Count > maxCount)
                base.InnerList.RemoveAt(0);
        }

        public void Remove(string file)
        {
            base.InnerList.Remove(file);
        }

        public void MoveFront(string file)
        {
            this.Remove(file);
            this.Add(file);
        }

        public bool Find(string file)
        {
            foreach (string temp in base.InnerList)
            {
                if (temp.Equals(file))
                    return true;
            }

            return false;
        }

        public string this[int index]
        {
            get { return (string)base.InnerList[index]; }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MRUCollection));

            if (File.Exists(mruFile))
                File.Delete(mruFile);

            FileStream fs = new FileStream(mruFile, FileMode.CreateNew, FileAccess.Write);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static MRUCollection FromFile(string fileName)
        {
            mruFile = fileName;
            if (!File.Exists(fileName))
                return new MRUCollection();

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(MRUCollection));
            MRUCollection mruCollection = (MRUCollection)serializer.Deserialize(fs);
            fs.Close();

            return mruCollection;
        }
    }
}
