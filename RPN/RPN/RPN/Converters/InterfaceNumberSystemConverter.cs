namespace RPN.Converters
{
public interface InterfaceConvertersConverter: InterfaceDictKey
{
    int ToDecimal(string number);
    string FromDecimal(int number);
}

}
