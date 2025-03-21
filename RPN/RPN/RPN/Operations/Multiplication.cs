namespace RPN.Operations
{
    public class Multiplication : InterfaceOperation
    {
        public int Execute(int[] operands)
        {
            return checked(operands[0] * operands[1]);
        }

        public int OperandCount => 2;
        public string Symbol => "*";
    }
}
