namespace RPN.Converters
{
    public class BinaryConverter : InterfaceConvertersConverter
    {
        public int ToDecimal(string number)
        {
            if (string.IsNullOrEmpty(number) || !IsBinary(number))
            {
                throw new ArgumentException($"Invalid binary number: {number}");
            }
            return Convert.ToInt32(number, 2);
        }

        public string FromDecimal(int number)
        {
            return Convert.ToString(number, 2);
        }

        public string Symbol
        {
            get { return "B"; }
        }

        private bool IsBinary(string number)
        {
            foreach (char c in number)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
