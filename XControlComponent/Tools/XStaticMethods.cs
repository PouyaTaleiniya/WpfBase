namespace XControlHelper
{
    public static class XStaticMethods
    {
        public static string FillStringSafe(this object Value, int Limit = 0)
        {
            var stringValue = string.Empty;
            if (Value != null)
            {
                stringValue = Value.ToString().Trim();
                if (Limit > 0)
                    stringValue = stringValue.Substring(0, Limit) + "...";
            }
            return stringValue;
        }

        public static bool IsNullOrEmpty(this object Value)
        {
            if (Value == null)
                return true;
            return string.IsNullOrEmpty(Value.ToString()) || string.IsNullOrWhiteSpace(Value.ToString());
        }

        public static bool IsStringEqualParamValues(this string Value, params string[] Values)
        {
            return Values.ToList().FirstOrDefault(a => a.Trim() == Value.Trim()) != null;
        }

        public static bool IsStringContainValues(this string Value, params string[] Values)
        {
            return Values.ToList().FirstOrDefault(a => a.Trim().Contains(Value.Trim())) != null;
        }

        public static bool IsStringEqualValues(this string Value, string Values)
        {
            var getValuesArray = Values.Split(',').ToArray();
            return getValuesArray.ToList().FirstOrDefault(a => a.Trim() == Value.Trim()) != null;
        }

        public static bool IsStringContainValues(this string Value, string Values)
        {
            var getValuesArray = Values.Split(',').ToArray();
            return getValuesArray.ToList().FirstOrDefault(a => getValuesArray.Contains(a.Trim())) != null;
        }
    }
}
