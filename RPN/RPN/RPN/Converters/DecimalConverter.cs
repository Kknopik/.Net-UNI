namespace RPN.Converters
{
    public class DecimalConverter : InterfaceConvertersConverter
    {
        public int ToDecimal(string number)
        {
            if (!int.TryParse(number, out int result))
            {
                throw new ArgumentException($"Invalid decimal number: {number}");
            }
            return result;
        }

        public string FromDecimal(int number)
        {
            return "D" + Convert.ToString(number, 10).ToUpper();
        }

        public string Symbol
        {
            get { return "D"; }
        }
    }
}
