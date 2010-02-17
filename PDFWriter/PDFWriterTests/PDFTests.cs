﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using NUnit.Framework;

using PDFWriter;

namespace PDFWriterTests
{
    [TestFixture]
    class PDFTests
    {
        [Test]
        public void Test1PagePDF()
        {
            DataSet data = new DataSet("Sample");
            data.ReadXml("../../1page.xml");

            PDFWriter.PDFWriter.PageLayout = new PageLayout();

            string tmp = PDFWriter.PDFWriter.GetPDF(data);

            StreamWriter fileWriter = new StreamWriter("../../1page.pdf");
            fileWriter.Write(tmp);
            fileWriter.Close();

            StreamReader file = new StreamReader("../../1page.pdf");
            string pdf = file.ReadToEnd();
            file.Close();

            Assert.AreEqual(pdf, tmp);
        }

        [Test]
        public void Test2PagesPDF()
        {
        }

        [Test]
        public void Test3PagesPDF()
        {
        }
    }
}
