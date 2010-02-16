﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFDocument : PDFStructureObject
    {
        private int _count = 1;

        public PDFDocument()
        {
        }

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

        public void AddChild(PDFStructureObject pdfObject)
        {
            _childs.Add(pdfObject);
            pdfObject.ObjectNumber = _count++;
        }

        public override string ToInnerPDF()
        {
            string xref = string.Empty;
            string pdfString = "%PDF-1.3\n";
            foreach (PDFStructureObject pdfObject in _childs)
            {
                xref += string.Format("{0:0000000000} 00000 n\n", pdfString.Length);
                pdfString += pdfObject.ToInnerPDF();
            }

            return string.Format(
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
