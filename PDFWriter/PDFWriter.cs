﻿namespace PDF
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// PDF writer library: generates a PDF file from a DataSet, see PDFWriter and PDFDocument for more documentation.
    /// </summary>
    /// <see cref="PDFWriter"/>
    /// <see cref="PDFDocument"/>
    /// 
    /// @mainpage
    public static class NamespaceDoc
    {
        // Special trick to document the namespace
        // See http://stackoverflow.com/questions/793210/c-xml-documentation-for-a-namespace
    }

    /// <summary>
    /// PDF writer library: generates a PDF file from a DataSet.
    /// </summary>
    /// 
    /// <remarks>
    /// See <a href="http://en.wikipedia.org/wiki/PDF_format">Wikipedia page about PDF</a><br/>
    /// For more technical informations about the PDF format, see PDFDocument.<br/>
    /// <br/>
    /// PDFWriter has been tested with <a href="http://get.adobe.com/reader/">Acrobat Reader 9.1.0</a>,
    /// <a href="http://www.foxitsoftware.com/">Foxit Reader 2.3</a> and
    /// <a href="http://blog.kowalczyk.info/software/sumatrapdf/index.html">Sumatra PDF 1.0.1</a><br/>
    /// <br/>
    /// 
    /// Other C# PDF libraries (2010/03/12):<br/>
    /// <br/>
    /// <a href="http://csharp-source.net/open-source/pdf-libraries">Open Source PDF Libraries in C#</a>
    /// <br/>
    /// - <a href="http://sourceforge.net/projects/itextsharp/">iTextSharp</a><br/>
    /// Affero GNU Public License/Commercial license<br/>
    /// Last version: 5.0.0 2009-12-08<br/>
    /// Last commit: 2010-01-04<br/>
    /// 93% of 141 users recommend this project<br/>
    /// <br/>
    /// - <a href="http://www.pdfsharp.net/">PDFsharp</a><br/>
    /// MIT<br/>
    /// Last version: 1.31 2009-12-09<br/>
    /// No repository?<br/>
    /// 90% of 20 users recommend this project<br/>
    /// <br/>
    /// - <a href="http://sourceforge.net/projects/report/">Report.NET</a><br/>
    /// LGPL<br/>
    /// Last version: 0.09.05 2006-11-13<br/>
    /// No repository<br/>
    /// 100% of 8 users recommend this project<br/>
    /// <br/>
    /// - <a href="http://pdfjet.com/net/index.html">PDFjet for .NET</a><br/>
    /// Evaluation License<br/>
    /// <br/>
    /// - <a href="http://sourceforge.net/projects/npdf/">ASP.NET FO PDF</a><br/>
    /// LGPL<br/>
    /// Last version: 1.0.1439.19630 2003-12-16<br/>
    /// No repository<br/>
    /// 100% of 1 user recommends this project<br/>
    /// <br/>
    /// - <a href="http://sourceforge.net/projects/clown/">PDF Clown</a><br/>
    /// LGPL<br/>
    /// Last version: 0.0.7-Alpha 2009-01-02<br/>
    /// No repository<br/>
    /// 75% of 4 users recommend this project<br/>
    /// <br/>
    /// 
    /// In order to debug PDF files generated by PDFWriter, you can use <a href="http://mupdf.com/">MuPDF</a> tools.
    /// <br/>
    /// This class implements the algorithms that use PDFGraphicObjects and PDFStructureObjects
    /// in order to create a PDF file. The main difficulty is to split DataSet rows on several pages.
    /// <br/>
    /// Main method is Page.CreatePages(), other methods available inside classes Page and Table
    /// are just helper methods.<br/>
    /// <br/>
    /// TODO: remove hardcoded values, add compression, add templates.
    /// </remarks>
    public static class PDFWriter
    {
        /// <summary>
        /// Default font.
        /// </summary>
        public static Font DefaultFont
        {
            // TODO use a style template to get this property
            get { return new Font(Font.Helvetica, 9); }
        }

        /// <summary>
        /// Default bold font.
        /// </summary>
        public static Font DefaultBoldFont
        {
            // TODO use a style template to get this property
            get { return new Font(Font.HelveticaBold, 9); }
        }

        /// <summary>
        /// Title font.
        /// </summary>
        public static Font TitleFont
        {
            // TODO use a style template to get this property
            get { return new Font(Font.HelveticaBold, 14, Color.Green); }
        }

        /// <summary>
        /// A PDF cellule background color, see PDFTextBox.
        /// </summary>
        public static string CellBackgroundColor
        {
            get
            {
                // TODO use a style template to get this property
                return Color.Silver;
            }
        }

        private static readonly List<PDFFont> _fonts = new List<PDFFont>();

        /// <summary>
        /// Gets the list of available fonts as a list of PDF objects.
        /// </summary>
        public static List<PDFFont> Fonts
        {
            get
            {
                if (_fonts.Count == 0)
                {
                    Dictionary<string, string> fontDictionary = Font.PDFFonts;
                    foreach (KeyValuePair<string, string> pair in fontDictionary)
                    {
                        PDFFont font = new PDFFont(pair.Key, pair.Value);
                        _fonts.Add(font);
                    }
                }
                return _fonts;
            }
        }

        /// <summary>
        /// Main function: gets the PDF given a DataSet.
        /// </summary>
        /// <param name="data">DataSet to convert into a PDF.</param>
        /// <returns>The PDF.</returns>
        public static string GetPDF(DataSet data)
        {
            // Root
            PDFDocument doc = new PDFDocument();
            ////

            // Info
            PDFInfo info = new PDFInfo("Report", "PDFWR", "PDFWR");
            doc.Info = info;
            doc.AddChild(info);
            ////

            // Fonts
            foreach (PDFFont font in Fonts)
            {
                doc.AddChild(font);
            }
            ////

            // Outlines
            PDFOutlines outlines = new PDFOutlines();
            doc.AddChild(outlines);
            ////

            // Pages
            PDFPages pages = Page.CreatePages(data, doc, outlines);
            doc.AddChild(pages);
            ////

            // Add headers and footers
            int count = 1;
            foreach (PDFPage page in pages.Pages)
            {
                List<PDFGraphicObject> header = Page.CreateHeader();
                page.ContentStream.AddRange(header);

                List<PDFGraphicObject> footer = Page.CreateFooter(count, pages.Pages.Count);
                page.ContentStream.AddRange(footer);

                count++;
            }
            ////

            // Catalog
            PDFCatalog catalog = new PDFCatalog(outlines, pages);
            doc.Catalog = catalog;
            doc.AddChild(catalog);
            ////

            return doc.ToInnerPDF();
        }
    }
}
