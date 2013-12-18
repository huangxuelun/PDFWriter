﻿namespace PDFWriterApp
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;

    using PDF;

    /// <summary>
    /// PDFWriter application that takes an .xml file (DataSet)
    /// and generates a PDF file.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlFileName = string.Empty;
            if (args.Count() > 0)
            {
                xmlFileName = args[0];
                Console.WriteLine("Input DataSet: " + xmlFileName);
            }

            string pdfFileName = string.Empty;
            if (args.Count() > 1)
            {
                pdfFileName = args[1];
                Console.WriteLine("Output PDF: " + pdfFileName);
            }

            if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(pdfFileName))
            {
                Console.WriteLine("Usage: pdfwriter input.xml output.pdf");
            }
            else
            {
                DataSet data = new DataSet();
                data.ReadXml(xmlFileName);

                Console.WriteLine("Generating PDF...");
                string pdf = PDFWriter.GetPDF(data);

                Console.WriteLine("Writing PDF file...");
                StreamWriter file = new StreamWriter(pdfFileName);
                file.Write(pdf);
                file.Close();
            }
        }
    }
}
