﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFCatalog : PDFObject
    {
        public PDFOutlines Outlines
        {
            get;
            set;
        }

        public PDFPages Pages
        {
            get;
            set;
        }

        public override string ToInnerPDF()
        {
            return string.Format(@"
% PDFCatalog (
{0} 0 obj
    <<
        /Type /Catalog
        /Outlines {1} 0 R
        /Pages {2} 0 R
    >>
endobj
%)
", ObjectNumber, Outlines.ObjectNumber, Pages.ObjectNumber
            );
        }

    }
}
