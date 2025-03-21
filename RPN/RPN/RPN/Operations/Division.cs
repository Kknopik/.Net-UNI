namespace RPN.Operations
{
    public class Division : InterfaceOperation
    {
        public int Execute(int[] operands)
        {
            if (operands[1] == 0)
            {
                throw new DivideByZeroException("Division by zero prohibited.");
            }
            return operands[0] / operands[1];
        }
        
        public int OperandCount => 2;
        public string Symbol => "/";
    }
}
