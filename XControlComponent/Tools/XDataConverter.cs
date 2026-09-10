using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace XControlHelper
{
    public static class XDataConverter
    {
        public static bool Regex_Int(object Text)
        {
            return !Regex.IsMatch(Text.FillStringSafe(), @"^[0-9]+$");
        }

        public static bool Regex_Decimal(object Text, XDecimalPoints DecimalPoint = XDecimalPoints.Zero)
        {
            var RegexMatch = @"^\d*\.?\d*$";
            if (DecimalPoint > 0)
                RegexMatch = @"^\d*(\.\d{0," + (int)DecimalPoint + "})?$";
            return !Regex.IsMatch(Text.FillStringSafe(), RegexMatch);
        }

        public static bool Regex_Persian(object Text, bool IsNumber, bool IsSpecialCharacter)
        {
            var CharData = "\\u0600-\\u06FF\\uFB8A\\u067E\\u0686\\u06AF\\u06CC";
            if (IsNumber)
                CharData += "\\0-9";
            if (IsSpecialCharacter)
                CharData += GetSpecialCharacter();
            return !Regex.IsMatch(Text.FillStringSafe(), @"^[" + CharData + "]+$");
        }

        public static bool Regex_English(object Text, bool IsNumber, bool IsSpecialCharacter)
        {
            var CharData = "a-zA-Z";
            if (IsNumber)
                CharData += "0-9";
            if (IsSpecialCharacter)
                CharData += GetSpecialCharacter();
            return !Regex.IsMatch(Text.FillStringSafe(), @"^[" + CharData + "]+$");
        }

        public static bool ContainsEmoji(string text)
        {
            foreach (char c in text)
            {
                // بررسی با UnicodeCategory
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category == UnicodeCategory.OtherSymbol || category == UnicodeCategory.Surrogate)
                    return true;

                int codePoint = char.ConvertToUtf32(text, text.IndexOf(c));
                if ((codePoint >= 0x1F600 && codePoint <= 0x1F64F) ||     // Emoticons
                    (codePoint >= 0x1F300 && codePoint <= 0x1F5FF) ||     // Misc Symbols and Pictographs
                    (codePoint >= 0x1F680 && codePoint <= 0x1F6FF) ||     // Transport & Map
                    (codePoint >= 0x2600 && codePoint <= 0x26FF) ||       // Misc symbols
                    (codePoint >= 0x2700 && codePoint <= 0x27BF) ||       // Dingbats
                    (codePoint >= 0x1F900 && codePoint <= 0x1F9FF))       // Supplemental Symbols and Pictographs
                {
                    return true;
                }
            }
            return false;
        }

        private static string GetSpecialCharacter()
        {
            return "!@#$%^&*()_+\\-=\\[\\]{};':\".,<>/?\\\\|`~@";
        }

        public static List<string> GetListBySplit(this string Value, string SplitChar)
        {
            var result = new List<string>();
            if (Value.IsNullOrEmpty())
                return result;
            result = Value.Split(SplitChar.ToCharArray()).Where(a => !a.IsNullOrEmpty()).ToList();
            return result;
        }

        public static string GetStringBySplit(this List<string> model, string SplitChar)
        {
            var result = string.Empty;
            if (model.Count() == 0)
                return result;
            model.ForEach(item =>
            {
                result += item + SplitChar;
            });
            return result;
        }

        public static string GetStringBySplit(this List<string> model, string Start, string End, string SplitChar)
        {
            var result = string.Empty;
            if (model.Count() == 0)
                return result;
            model.ForEach(item =>
            {
                result += Start + item + End + SplitChar;
            });
            return result;
        }

        public static int IntParse(this object Value)
        {
            var ValueData = 0;

            if (Value.IsNullOrEmpty())
                return 0;

            if (int.TryParse(Value.ToString(), out int T))
                ValueData = int.Parse(Value.ToString());

            return ValueData;
        }

        public static int GetIntPercent(decimal Value, decimal Percent)
        {
            if (Value == 0 || Percent == 0)
                return 0;

            var getPercent = (Value * Percent) / 100;
            if (getPercent.ToString().Contains("."))
                return getPercent.ToString().Split('.')[0].IntParse();

            return getPercent.IntParse();
        }

        public static int ConvertMillimeterToPixel(double millimeter, int dpi)
        {
            // Calculate pixels based on DPI
            double inches = millimeter / 25.4; // Convert millimeters to inches
            int pixels = (int)Math.Round(inches * dpi);

            return pixels;
        }

        public static long LongParse(this object Value)
        {
            long ValueData = 0;

            if (Value.IsNullOrEmpty())
                return 0;

            if (long.TryParse(Value.ToString(), out long T))
                ValueData = long.Parse(Value.ToString());

            return ValueData;
        }

        public static float FloatParse(this object Value)
        {
            float ValueData = 0;

            if (Value == null)
                return 0;

            if (float.TryParse(Value.ToString(), out float T))
                ValueData = float.Parse(Value.ToString());

            return ValueData;
        }

        public static decimal DecimalParse(this object Value)
        {
            decimal ValueData = 0;

            if (Value == null)
                return 0;

            if (decimal.TryParse(Value.ToString(), out decimal T))
                ValueData = decimal.Parse(Value.ToString());

            return ValueData;
        }

        public static decimal DecimalParse(this object Value, XDecimalPoints DecimalPoint = XDecimalPoints.Zero)
        {
            decimal ValueData = 0;

            if (Value == null)
                return 0;

            if (decimal.TryParse(Value.ToString(), out decimal T))
            {
                if (DecimalPoint == XDecimalPoints.One)
                    ValueData = decimal.Parse(string.Format("{0:F1}", Value));
                else if (DecimalPoint == XDecimalPoints.Two)
                    ValueData = decimal.Parse(string.Format("{0:F2}", Value));
                else if (DecimalPoint == XDecimalPoints.Three)
                    ValueData = decimal.Parse(string.Format("{0:F3}", Value));
            }

            return ValueData;
        }

        public static decimal GetDecimalPercent(decimal Value, decimal Percent, XDecimalPoints DecimalPoint = XDecimalPoints.Zero)
        {
            if (Value == 0 || Percent == 0)
                return 0;

            var getValue = (Value * Percent) / 100;
            return getValue.DecimalParse(DecimalPoint);
        }

        public static bool BoolParse(this object Value)
        {
            var ValueData = false;

            if (Value == null)
                return false;

            if (bool.TryParse(Value.ToString(), out bool T))
                ValueData = bool.Parse(Value.ToString());

            return ValueData;
        }

        #region DateTime
        public static DateTime DateTimeParse(this object Value)
        {
            var ValueData = DateTime.Now;

            if (Value == null)
                return DateTime.MinValue;

            if (DateTime.TryParse(Value.ToString(), out DateTime T))
                ValueData = DateTime.Parse(Value.ToString());

            return ValueData;
        }

        public static DateTime StringToDateTime(this object _date, string FormatDate = "Y/M/D")
        {
            var getDateTime = DateTime.Now;
            if (_date.IsNullOrEmpty())
                return getDateTime;

            try
            {
                var getDates = _date.FillStringSafe().Split('/').ToList();
                int Y = 0;
                int M = 0;
                int D = 0;

                if(FormatDate == "Y/M/D")
                {
                    Y = int.Parse(getDates[0]);
                    M = int.Parse(getDates[1]);
                    D = int.Parse(getDates[2]);
                }
                if (FormatDate == "Y/D/M")
                {
                    Y = int.Parse(getDates[0]);
                    D = int.Parse(getDates[1]);
                    M = int.Parse(getDates[2]);
                }
                if (FormatDate == "D/M/Y")
                {
                    D = int.Parse(getDates[0]);
                    M = int.Parse(getDates[1]);
                    Y = int.Parse(getDates[2]);
                }
                if (FormatDate == "M/D/Y")
                {
                    M = int.Parse(getDates[0]);
                    D = int.Parse(getDates[1]);
                    Y = int.Parse(getDates[2]);
                }

                getDateTime = new DateTime(Y, M, D);
            }
            catch (Exception ex)
            {
                getDateTime = DateTime.Now;
            }

            return getDateTime;
        }

        public static string DateTimeToString(this DateTime _date, string FormatDate = "Y/M/D")
        {
            var getDate = string.Empty;
            var Y = _date.Year.ToString();

            var M = _date.Month.ToString();
            if (M.Length == 1)
                M = "0" + M;

            var D = _date.Day.ToString();
            if (D.Length == 1)
                D = "0" + D;

            return GetDateFormat(FormatDate, Y, M, D);
        }

        public static string GetCurrentDate(bool IsPersianMode, string FormatDate = "Y/M/D")
        {
            var Year = string.Empty;
            var Month = string.Empty;
            var Day = string.Empty;

            var getDateTime = DateTime.Now;
            if (IsPersianMode)
            {
                //PersianDate persianDate = new PersianDate(getDateTime.Year, getDateTime.Month, getDateTime.Day);
                //Year = persianDate.Year.ToString();
                //Month = persianDate.Month.ToString();
                //Day = persianDate.Day.ToString();

            }
            else
            {
                getDateTime = new DateTime(getDateTime.Year, getDateTime.Month, getDateTime.Day);
                Year = getDateTime.Year.ToString();
                Month = getDateTime.Month.ToString();
                Day = getDateTime.Day.ToString();
            }

            var Y = Year.ToString();

            var M = Month.ToString();
            if (M.Length == 1)
                M = "0" + M;

            var D = Day.ToString();
            if (D.Length == 1)
                D = "0" + D;
             
            return GetDateFormat(FormatDate, Y, M, D);
        }

        public static string GetDateFormat(string FormatDate, string Year, string Month, string Day)
        {
            if (FormatDate == "D/M/Y")
                return Day + "/" + Month + "/" + Year;

            if (FormatDate == "M/D/Y")
                return Month + "/" + Day + "/" + Year;

            if (FormatDate == "Y/D/M")
                return Year + "/" + Day + "/" + Month;

            return Year + "/" + Month + "/" + Day;
        }

        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm");
        }
        #endregion

        public static Guid GuidParse(this object Value)
        {
            var ValueData = Guid.Empty;
            if (Value.IsNullOrEmpty())
                return ValueData;

            if (Guid.TryParse(Value.ToString(), out Guid T))
                ValueData = Guid.Parse(Value.ToString());

            return ValueData;
        }

        public static DataTable ConvertListToDataTable<T>(List<T> list)
        {
            DataTable dataTable = new DataTable();

            if (list.Count > 0)
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (var item in list)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }


    }
}
