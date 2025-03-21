
namespace RPN;

public interface InterfaceOperation : InterfaceDictKey
{
    int Execute(int[] operands);
    int OperandCount { get; }
}

