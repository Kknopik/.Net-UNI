namespace RPN.Converters
{
    public class HexadecimalConverter : InterfaceConvertersConverter
    {
        public int ToDecimal(string number)
        {
            if (string.IsNullOrEmpty(number) || !IsHexadecimal(number))
            {
                throw new ArgumentException($"Invalid hexadecimal number: {number}");
            }
            return Convert.ToInt32(number, 16);
        }

        public string FromDecimal(int number)
        {
            return Convert.ToString(number, 16).ToUpper();
        }

        public string Symbol
        {
            get { return "#"; }
        }
        private bool IsHexadecimal(string number)
        {
            foreach (char c in number)
            {
                if (!Uri.IsHexDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
