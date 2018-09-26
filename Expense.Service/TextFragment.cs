using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Service
{
    /// <summary>
    /// TextFragment object represents a fragment of expense text according to its type in the expense text.
    /// </summary>
    public class TextFragment
    {
        /// <summary>
        /// Fragment type
        /// </summary>
        public TextFragmentType Type { get; set; }

        /// <summary>
        /// Raw text in the expense text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// MeaningfulText trims the "<>" for XML starting tag, and trims the "</>" for XML closing tag.
        /// </summary>
        public string MeaningfulText
        {
            get
            {
                switch (Type)
                {
                    case TextFragmentType.XmlStartTag:
                        return Text.TrimStart('<').TrimEnd('>');
                    case TextFragmentType.XmlCloseTag:
                        return Text.Replace("</", "").TrimEnd('>');
                    case TextFragmentType.PlainText:
                        return Text.Replace("\r\n", " ");
                    default:
                        throw new ApplicationException("Text fragment type not supported"); 
                }
            }
        }
    }
}
