// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Silverlight.Testing.Html
{
    /// <summary>
    /// Represents the font properties for text. This class cannot be inherited.
    /// </summary>
    public class FontInfo 
    {
        /// <summary>
        /// The owning control of this font information.
        /// </summary>
        private HtmlControlBase _owner;

        /// <summary>
        /// Initializes a FontInfo object.
        /// </summary>
        /// <param name="owner">The owning Control reference.</param>
        public FontInfo(HtmlControlBase owner) 
        {
            _owner = owner;
        }

        /// <summary>
        /// Set a value on the owning HtmlElement.
        /// </summary>
        /// <param name="property">The CSS property.</param>
        /// <param name="val">The value.</param>
        private void SetCssValue(CssAttribute property, string val)
        {
            _owner.SetStyleAttribute(property, val);
        }

        /// <summary>
        /// Gets a CSS value.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <returns>Returns the actual value as a string.</returns>
        private string GetCssValue(CssAttribute property)
        {
            string val = null;
            try
            {
                string str = _owner.GetStyleAttribute(property);
                val = str;
            }
            catch (ArgumentException)
            {
            }
            return val;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text is bold.
        /// </summary>
        public bool Bold 
        {
            get 
            {
                return "bold" == GetCssValue(CssAttribute.FontWeight);
            }

            set 
            {
                SetCssValue(CssAttribute.FontWeight, value == true ? "bold" : "normal");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text is italic.
        /// </summary>
        public bool Italic 
        {
            get 
            {
                return "italic" == GetCssValue(CssAttribute.FontStyle);
            }

            set 
            {
                SetCssValue(CssAttribute.FontStyle, value == true ? "italic" : "normal");
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        public string Name 
        {
            get 
            {
                string[] names = Names;
                if (names.Length > 0)
                {
                    return names[0];
                }
                return String.Empty;
            }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (value.Length == 0) 
                {
                    Names = null;
                }
                else 
                {
                    Names = new string[1] { value };
                }
            }
        }

        /// <summary>
        /// Gets or sets the font names.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "The type converter must return a string []")]
        [TypeConverterAttribute(typeof(FontNamesConverter))]
        public string[] Names 
        {
            get 
            {
                FontNamesConverter convert = new FontNamesConverter();
                string fontNames = GetCssValue(CssAttribute.FontFamily);
                return String.IsNullOrEmpty(fontNames) ? new string[0] : (string[])convert.ConvertFromString(fontNames);
            }

            set 
            {
                FontNamesConverter convert = new FontNamesConverter();
                string singleString = (string)convert.ConvertTo(value, typeof(string));
                SetCssValue(CssAttribute.FontFamily, singleString);
            }
        }

        // [DefaultValue(typeof(FontUnit), "")]

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public FontUnit Size 
        {
            get 
            {
                string fs = GetCssValue(CssAttribute.FontSize);
                if (!String.IsNullOrEmpty(fs))
                {
                    FontUnit fontUnit = FontUnit.Parse(fs, CultureInfo.InvariantCulture);
                    return fontUnit;
                }
                return FontUnit.Empty;
            }

            set 
            {
                if ((value.Type == FontSize.AsUnit) && (value.Unit.Value < 0)) 
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                SetCssValue(CssAttribute.FontSize, value.ToString());
            }
        }

        // [DefaultValue(false)]

        /// <summary>
        /// Gets or sets a value indicating whether the text has a line through it.
        /// </summary>
        public bool Strikeout 
        {
            get 
            {
                return "line-through" == GetCssValue(CssAttribute.TextDecoration);
            }

            set
            {
                // one-way setter
                if (value)
                {
                    SetCssValue(CssAttribute.TextDecoration, "line-through");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text is underlined.
        /// </summary>
        public bool Underline 
        {
            get
            {
                return "underline" == GetCssValue(CssAttribute.TextDecoration);
            }

            set
            {
                if (value)
                {
                    SetCssValue(CssAttribute.TextDecoration, "underline");
                }
                else
                {
                    SetCssValue(CssAttribute.TextDecoration, "none");
                }
            }
        }

        /// <summary>
        /// Copies the font properties of another FontInfo into this instance.
        /// </summary>
        /// <param name="font">Object to copy from.</param>
        public void CopyFrom(FontInfo font)
        {
            if (font != null)
            {
                Names = font.Names;
                Size = font.Size;
                Bold = font.Bold;
                Italic = font.Italic;
                Strikeout = font.Strikeout;
                Underline = font.Underline;
            }
        }

        /// <summary>
        /// Copies the font properties of another FontInfo into this instance.
        /// </summary>
        /// <param name="info">Object to copy from.</param>
        public void CopyFontNamesAndSizeFrom(FontInfo info) 
        {
            if (info != null) 
            {
                Names = info.Names;
                Size = info.Size;
            }
        }

        /// <summary>
        /// Combines the font properties of another FontInfo with this instance.
        /// </summary>
        /// <param name="font">Object to merge values with.</param>
        public void MergeWith(FontInfo font) 
        {
            if (font != null) 
            {
                Names = font.Names;
                Size = font.Size;
                Bold = font.Bold;
                Italic = font.Italic;
                Strikeout = font.Strikeout;
                Underline = font.Underline;
            }
        }

        /// <summary>
        /// The string value of the FontInfo instance.
        /// </summary>
        /// <returns>Returns a string.</returns>
        public override string ToString() 
        {
            string size = this.Size.ToString();
            string s = this.Name;
            if (size.Length != 0) 
            {
                if (s.Length != 0) 
                {
                    s += ", " + size;
                }
                else 
                {
                    s = size;
                }
            }
            return s;
        }
    }
}