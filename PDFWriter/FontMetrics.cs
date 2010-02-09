﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFWriter
{
    class FontMetrics
    {
        /// <summary>
        /// Helvetica
        /// </summary>
        private static double[] FH = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, 
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333, 
            .278, .278, .355, .556, .556, .889, .667, .191, .333, .333, .389, .584, .278, .333, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .278, .278, .584, .584, .584, .556, 
            1.015, .667, .667, .722, .722, .667, .611, .778, .722, .278, .500, .667, .556, .833, .722, .778, 
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .278, .278, .278, .469, .556, 
            .333, .556, .556, .500, .556, .556, .278, .556, .556, .222, .222, .500, .222, .833, .556, .556, 
            .556, .556, .333, .500, .278, .556, .500, .722, .500, .500, .500, .334, .260, .334, .584, .278, 
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .333, .333, .333, .222, 
            .222, .222, 1.00, .500, .500, .556, 1.00, .667, .667, .611, .278, .222, .944, .500, .500, .278, 
            .278, .333, .556, .556, .556, .556, .260, .556, .333, .737, .370, .556, .584, .278, .737, .333, 
            .400, .584, .333, .333, .333, .556, .537, .278, .333, .333, .365, .556, .834, .834, .834, .611, 
            .667, .667, .667, .667, .667, .667, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278, 
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611, 
            .556, .556, .556, .556, .556, .556, .889, .500, .556, .556, .556, .556, .278, .278, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .584, .611, .556, .556, .556, .556, .500, .556, .500, 
        };

        /// <summary>
        /// Helvetica Bold
        /// </summary>
        private static double[] FHB = new double[] {
            .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, .278, 
            .278, .278, .278, .278, .278, .278, .278, .278, .333, .333, .333, .333, .333, .333, .333, .333, 
            .278, .333, .474, .556, .556, .889, .722, .238, .333, .333, .389, .584, .278, .333, .278, .278, 
            .556, .556, .556, .556, .556, .556, .556, .556, .556, .556, .333, .333, .584, .584, .584, .611, 
            .975, .722, .722, .722, .722, .667, .611, .778, .722, .278, .556, .722, .611, .833, .722, .778, 
            .667, .778, .722, .667, .611, .722, .667, .944, .667, .667, .611, .333, .278, .333, .584, .556, 
            .333, .556, .611, .556, .611, .556, .333, .611, .611, .278, .278, .556, .278, .889, .611, .611, 
            .611, .611, .389, .556, .333, .611, .556, .778, .556, .556, .500, .389, .280, .389, .584, .278, 
            .350, .556, .556, 1.00, 1.00, .556, .556, .167, .333, .333, .584, 1.00, .500, .500, .500, .278, 
            .278, .278, 1.00, .611, .611, .611, 1.00, .667, .667, .611, .278, .278, .944, .556, .500, .278, 
            .278, .333, .556, .556, .556, .556, .280, .556, .333, .737, .370, .556, .584, .278, .737, .333, 
            .400, .584, .333, .333, .333, .611, .556, .278, .333, .333, .365, .556, .834, .834, .834, .611, 
            .722, .722, .722, .722, .722, .722, 1.00, .722, .667, .667, .667, .667, .278, .278, .278, .278, 
            .722, .722, .778, .778, .778, .778, .778, .584, .778, .722, .722, .722, .722, .667, .667, .611, 
            .556, .556, .556, .556, .556, .556, .889, .556, .556, .556, .556, .556, .278, .278, .278, .278, 
            .611, .611, .611, .611, .611, .611, .611, .584, .611, .611, .611, .611, .611, .556, .611, .556, 
        };

        public static double GetTextWidth(string text, Font font)
        {
            double width = 0;

            foreach (char ch in text)
            {
                int asciiCode = (int)ch;
                double ws = 0;
                if (ch == 32)
                {
                    //Width of a space character
                    ws = font.WordSpace;
                }
                double charWidth = 0;
                if (font.Name == Font.Helvetica)
                {
                    charWidth = FH[ch];
                }
                else if (font.Name == Font.HelveticaBold)
                {
                    charWidth = FHB[ch];
                }
                width += charWidth + font.CharSpace + ws;
            }

            return width * font.Size;
        }
    }
}