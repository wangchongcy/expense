using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Expense.Model;
using Expense.Model.Exceptions;

namespace Expense.Service
{
    /// <summary>
    /// A service that handles expense business logic
    /// </summary>
    public class ExpenseService : IExpenseService
    {
        /// <summary>
        /// Extract expense info from a block of text
        /// </summary>
        /// <param name="text">Block of text</param>
        /// <returns>Expense info domain object</returns>
        public ExpenseInfo ExtractExpenseInfoFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"Invalid parameter in method {nameof(ExtractExpenseInfoFromText)}"); 
            }

            // Xml start tag, close tag or plain text regular expression. 
            // change to @"<([0-9a-zA-Z\._-]*)>[^<>]*<\/\1>" to improve
            Regex regex = new Regex(
                @"(<[a-zA-Z_][0-9a-zA-Z\._-]*>)|(</[a-zA-Z_][0-9a-zA-Z\._-]*>)|([^<>]*)", 
                RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);

            if (matches.Count > 0)
            {
                var textFragments = new List<TextFragment>(); 
                foreach (Match match in matches)
                {
                    textFragments.Add(ToTextFragment(match.Value)); 
                }

                return ExtractExpenseInfoFromTextInternal(textFragments); 
            }
            else
            {
                throw new ApplicationException("Cannot match pattern from input text."); 
            }
        }

        private ExpenseInfo ExtractExpenseInfoFromTextInternal(List<TextFragment> textFragments)
        {
            var result = new ExpenseInfo(); 

            // If a property has not completed matching, it will be pushed to property stack. 
            var propertyStack = new Stack<StatedExpenseProperty>();

            // If a property has already matched, but it has parent property that is not completed 
            // matched, it will be added to matchedPropertyList.
            var matchedPropertyList = new List<StatedExpenseProperty>();

            // Indicate the type of last fragment.
            TextFragmentType lastFragmentType = TextFragmentType.XmlCloseTag; 

            foreach (var textFragment in textFragments)
            {
                switch (textFragment.Type) 
                {
                    case TextFragmentType.PlainText:
                        {
                            // A plain text can become property value only if last fragment type is xml
                            // open tag and property stack has not completed matching. 
                            if (lastFragmentType == TextFragmentType.XmlStartTag && 
                                propertyStack.Count != 0)
                            {
                                var property = propertyStack.Peek();
                                if (property.State == PropertyMatchingState.NameAssigned)
                                {
                                    property.Value = textFragment.MeaningfulText;
                                    property.State = PropertyMatchingState.ValueAssigned; 
                                }
                                else
                                {
                                    propertyStack.Pop();
                                }
                            }

                            lastFragmentType = TextFragmentType.PlainText; 
                            break;
                        }
                    case TextFragmentType.XmlStartTag:
                        {
                            // If a text fragment is xml start tag, push it to match stack. 
                            var property = new StatedExpenseProperty();
                            property.Name = textFragment.MeaningfulText;
                            property.State = PropertyMatchingState.NameAssigned;
                            propertyStack.Push(property);

                            lastFragmentType = TextFragmentType.XmlStartTag; 
                            break;
                        }
                    case TextFragmentType.XmlCloseTag:
                        {
                            if (propertyStack.Count != 0)
                            {
                                // If a text fragment is xml end tag, check if a property is matched.
                                var property = propertyStack.Pop();
                                if (property.State == PropertyMatchingState.ValueAssigned && 
                                    property.Name == textFragment.MeaningfulText)
                                {
                                    matchedPropertyList.Add(property);
                                }

                                // Finished matching a xml tree.
                                if (propertyStack.Count == 0)
                                {
                                    matchedPropertyList.ForEach(p => result.AddProperty(p));
                                    matchedPropertyList.Clear(); 
                                }
                            }

                            lastFragmentType = TextFragmentType.XmlCloseTag; 
                            break;
                        }
                    default:
                        throw new ApplicationException("Unsupported ExpenseTextUnitType."); 
                }
            }

            // if property stack has unmatched property, should reject the whole message.
            if (propertyStack.Count != 0)
            {
                throw new XmlTagNotMatchingException(); 
            }

            // If property list has no "total" property, throws exception. 
            if (!result.HasProperty("total"))
            {
                throw new MissingTotalPropertyException(); 
            }

            // If property list has no "cost_centre" property, default with "UNKNOWN" value.
            if (!result.HasProperty("cost_centre"))
            {
                result.AddProperty(new ExpenseProperty()
                {
                    Name = "cost_centre",
                    Value = "UNKNOWN"
                });
            }

            return result;
        }

        private TextFragment ToTextFragment(string text)
        {
            var result = new TextFragment();
            result.Text = text; 
            if (text.StartsWith("</"))
                result.Type = TextFragmentType.XmlCloseTag; 
            else if (text.StartsWith("<"))
                result.Type = TextFragmentType.XmlStartTag; 
            else
                result.Type = TextFragmentType.PlainText; 

            return result; 
        }
    }
}
