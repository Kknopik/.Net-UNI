
namespace RPN.Operations;


public class Addition : InterfaceOperation
{
    public int Execute(int[] operands) => operands[0] + operands[1];
    public int OperandCount => 2;
    public string Symbol => "+";

}
