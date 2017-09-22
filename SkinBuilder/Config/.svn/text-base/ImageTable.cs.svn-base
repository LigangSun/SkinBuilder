// ***************************************************************
//  ImageTable   version:  1.0   ? date: 02/12/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs: The images for special controls
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ZLIS.SkinBuilder
{
    public class ImageTable
    {
        private const int majorVersion = 1;
        private const int subVersion = 0;

        private ImageObject generalButtonImageObject = new ImageObject();
        private ImageObject generalCheckBoxImageObject = new ImageObject();
        private ImageObject generalRadioButtonImageObject = new ImageObject();
        private ImageObject generalComboBoxButtonImageObject = new ImageObject();

#region Properties
        public ImageObject GeneralButtonImageObject 
        {
            get { return this.generalButtonImageObject; }
        }

        public ImageObject GeneralCheckBoxImageObject 
        {
            get { return this.generalCheckBoxImageObject; }
        }

        public ImageObject GeneralRadioButtonImageObject 
        {
            get { return this.generalRadioButtonImageObject; }
        }

        public ImageObject GeneralComboBoxButtonImageObject 
        {
            get { return this.generalComboBoxButtonImageObject; }
        }
#endregion

        public void Load(string file)
        {
            if (!File.Exists(file))
                return;

            FileInfo fileInfo = new FileInfo(file);
            string path = fileInfo.Directory + @"\";
            using (XmlReader reader = XmlReader.Create(file))
            {
                this.generalButtonImageObject.Load(reader, path);
                this.generalCheckBoxImageObject.Load(reader, path);
                this.generalRadioButtonImageObject.Load(reader, path);
                this.generalComboBoxButtonImageObject.Load(reader, path);
            }
        }

        public void Save(string file)
        {
            if (!File.Exists(file))
                return;

            using (XmlWriter writer = XmlWriter.Create(file))
            {
                writer.WriteStartElement("GeneralButtonImageObject");
                this.generalButtonImageObject.Save(writer);
                writer.WriteEndElement();

                writer.WriteStartElement("GeneralCheckBoxImageObject");
                writer.WriteEndElement();

                writer.WriteStartElement("GeneralRadioButtonImageObject");
                writer.WriteEndElement();

                writer.WriteStartElement("GeneralComboBoxButtonImageObject");
                writer.WriteEndElement();
            }
        }
    }
}
