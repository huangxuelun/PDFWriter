﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// Represents the PDF and links to all the other PDF objects.
    /// This class is pretty important.
    /// </summary>
    /// 
    /// <remarks>
    /// See <a href="http://www.planetpdf.com/enterprise/article.asp?ContentID=6351">Anatomy of a PDF document</a><br/>
    /// See <a href="http://www.adobe.com/devnet/pdf/pdfs/PDFReference13.pdf">PDF format 1.3 full documentation (published in 2000)</a>
    /// Taken from <a href="http://www.adobe.com/devnet/pdf/pdf_reference_archive.html">http://www.adobe.com/devnet/pdf/pdf_reference_archive.html</a><br/>
    /// <br/>
    /// PDF format follows an object structure (see PDFStructureObject and subclasses).<br/>
    /// Each object are numbered (1 0 obj, 2 0 obj...).<br/>
    /// A comment starts with %.<br/>
    /// The most important PDF object is the content stream (PDFContentStream) that contains the text and graphics to show.<br/>
    /// <br/>
    /// The content stream uses a coordinate system (x, y):
    /// <code>
    /// y
    /// 
    /// 792
    /// ^
    /// |
    /// |
    /// |
    /// |
    /// |
    /// 0 --------> 612 x
    /// </code>
    /// 
    /// 612x792 being the default size of a PDF page.
    /// 
    /// Example of a PDF document:
    /// <code>
    /// <![CDATA[
    /// %PDF-1.3
    /// 
    /// % Generated by PDFCatalog (
    /// 1 0 obj
    /// 	<<
    /// 		% This object is the catalog
    /// 		/Type /Catalog
    /// 		% Links the outlines object (object number 2)
    /// 		/Outlines 2 0 R
    /// 		% Links to the list of pages object (object number 3)
    /// 		/Pages 3 0 R
    /// 	>>
    /// endobj
    /// % )
    /// 
    /// % Generated by PDFOutlines (
    /// 2 0 obj
    /// 	<<
    /// 		/Type /Outlines
    /// 		/Count 0
    /// 	>>
    /// endobj
    /// % )
    /// 
    /// % Generated by PDFPages (
    /// 3 0 obj
    /// 	<<
    /// 		/Type /Pages
    /// 		% Links to all the pages, here only one: object number 4
    /// 		/Kids [4 0 R]
    /// 		/Count 1
    /// 	>>
    /// endobj
    /// )
    /// 
    /// % Generated by PDFPage (
    /// 4 0 obj
    /// 	<<
    /// 		/Type /Page
    /// 		% Links to object Pages
    /// 		/Parent 3 0 R
    /// 		% Size of a page: 612x792
    /// 		/MediaBox [0 0 612 792]
    /// 		% Links to the content stream object (number 5)
    /// 		/Contents 5 0 R
    /// 		/Resources
    /// 			<<
    /// 				/ProcSet [/PDF /Text]
    /// 				/Font << /F1 6 0 R >>
    /// 			>>
    /// 	>>
    /// endobj
    /// % )
    /// 
    /// % This is the PDF content: text, graphics...
    /// % Generated by PDFContentStream (
    /// 5 0 obj
    /// 	<< /Length 73 >>
    /// stream
    /// 	% Shows string "Hello World" at position x=100, y=100 using font F1 size 24
    /// 	BT
    /// 		/F1 24 Tf
    /// 		100 100 Td
    /// 		(Hello World) Tj
    /// 	ET
    /// endstream
    /// endobj
    /// % )
    /// 
    /// % Generated by PDFFont (
    /// 6 0 obj
    /// 	<<
    /// 		/Type /Font
    /// 		/Subtype /Type1
    /// 		/Name /F1
    /// 		/BaseFont /Helvetica
    /// 		/Encoding /MacRomanEncoding
    /// 	>>
    /// endobj
    /// % )
    /// 
    /// % Reference table: references all the objects
    /// % Generated by PDFDocument
    /// xref
    /// 0 7
    /// 0000000000 65535 f
    /// 0000000009 00000 n
    /// 0000000074 00000 n
    /// 0000000120 00000 n
    /// 0000000179 00000 n
    /// 0000000364 00000 n
    /// 0000000496 00000 n
    /// 
    /// trailer
    /// 	<<
    /// 		/Size 8
    /// 		/Root 1 0 R
    /// 	>>
    /// 
    /// startxref
    /// 625
    /// 
    /// %%EOF
    /// ]]>
    /// </code>
    /// </remarks>
    class PDFDocument : PDFStructureObject
    {
        public PDFCatalog Catalog
        {
            get;
            set;
        }

        public PDFInfo Info
        {
            get;
            set;
        }

        private List<PDFStructureObject> _childs = new List<PDFStructureObject>();

        private int _count = 1;

        public void AddChild(PDFStructureObject pdfObject)
        {
            _childs.Add(pdfObject);
            pdfObject.ObjectNumber = _count++;
        }

        public override string ToInnerPDF()
        {
            System.Diagnostics.Trace.Assert(Catalog != null);
            System.Diagnostics.Trace.Assert(Info != null);
            System.Diagnostics.Trace.Assert(_childs.Count > 0);

            string xref = string.Empty;
            string pdfString = "%PDF-1.3\n";
            foreach (PDFStructureObject pdfObject in _childs)
            {
                xref += string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0000000000} 00000 n\n", pdfString.Length);
                pdfString += pdfObject.ToInnerPDF();
            }

            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
@"{0}

xref
0 {1}
0000000000 65535 f
{2}

trailer
    <<
        % Total number of entries in the file's cross-reference table[...] this
        % value is 1 greater than the highest object number used in the file.
        /Size {3}

        % The catalog object for the PDF document
        % contained in the file
        /Root {4} 0 R

        /Info {5} 0 R
    >>

startxref
{6}

%%EOF
", pdfString, _count - 1, xref, _count, Catalog.ObjectNumber, Info.ObjectNumber, pdfString.Length
            );
        }
    }
}
