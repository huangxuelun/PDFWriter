﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class PDFContentStream : PDFStructureObject
    {
        private List<PDFGraphicObject> _childs = new List<PDFGraphicObject>();

        public virtual void AddChild(PDFGraphicObject pdfGraphicObject)
        {
            _childs.Add(pdfGraphicObject);
        }

        public override string ToInnerPDF()
        {
            string tmp = string.Empty;
            foreach (PDFGraphicObject pdfGraphicObject in _childs)
            {
                tmp += pdfGraphicObject.ToInnerPDF();
            }

            return string.Format(@"
% PDFContentStream (
{0} 0 obj
    <<
        /Length {1}
    >>
stream
{2}
endstream
endobj
% )
", ObjectNumber, tmp.Length, tmp
            );
        }

    }
}
