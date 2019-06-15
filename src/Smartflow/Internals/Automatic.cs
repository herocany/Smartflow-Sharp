﻿/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Smartflow.Elements;

namespace Smartflow.Internals
{
    internal class Automatic : IResolution
    {
        public Workflow Parse(string resourceXml)
        {
            byte[] buffer = System.Text.ASCIIEncoding.UTF8.GetBytes(resourceXml);
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Workflow));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                //IgnoreComments
                Workflow o = (Workflow)serializer.Deserialize(ms);
                return o;
            }
        }
    }
}
