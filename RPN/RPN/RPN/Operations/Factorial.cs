namespace RPN.Operations
{
    public class Factorial : InterfaceOperation
    {
        public int Execute(int[] operands)
        {
            if (operands[0] < 0)
                throw new ArgumentException("Factorial: negative numbers not allowed");

            int result = 1;
            for (int i = 2; i <= operands[0]; i++)
                result *= i;

            return result;
        }
        
        public int OperandCount => 1;
        public string Symbol => "!";
    }
}
