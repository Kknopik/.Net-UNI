using RPN.Converters;

namespace RPN
{
    public class RpnEvaluator
    {
        private readonly MyStack<int> _operandStack = new();
        private readonly Dictionary<string, InterfaceOperation> _operationLookup;
        private readonly Dictionary<string, InterfaceConvertersConverter> _converterLookup;

        public RpnEvaluator(Dictionary<string, InterfaceOperation> operations, Dictionary<string, InterfaceConvertersConverter> converters)
        {
            _operationLookup = operations;
            _converterLookup = converters;
        }

        public int Evaluate(string input)
        {
            var tokens = input.Split(' ');
            foreach (var token in tokens)
            {
                if (IsNumeric(token))
                    _operandStack.Push(int.Parse(token));
                else if (IsOperation(token))
                    PerformOperation(token);
                else
                    ConvertToken(token);
            }

            var result = _operandStack.Pop();
            if (_operandStack.IsEmpty)
            {
                return result;
            }
            throw new InvalidOperationException("The stack should be empty after evaluation.");
        }

        private bool IsNumeric(string input) => int.TryParse(input, out _);

        private bool IsOperation(string token) => _operationLookup.ContainsKey(token);

        private void PerformOperation(string token)
        {
            var operation = RetrieveOperation(token);
            var operandValues = new int[operation.OperandCount];

            for (int i = operation.OperandCount - 1; i >= 0; i--)
            {
                if (_operandStack.IsEmpty)
                    throw new InvalidOperationException($"Insufficient operands for operator '{token}'.");
                operandValues[i] = _operandStack.Pop();
            }

            _operandStack.Push(operation.Execute(operandValues));
        }

        private InterfaceOperation RetrieveOperation(string symbol)
        {
            if (_operationLookup.TryGetValue(symbol, out var operation))
            {
                return operation;
            }
            throw new InvalidOperationException($"No operation found for symbol: {symbol}");
        }

        private void ConvertToken(string token)
        {
            if (token.Length > 1 && _converterLookup.ContainsKey(token[0].ToString()))
            {
                var systemIdentifier = token[0].ToString();
                var numberPart = token.Substring(1);
                _operandStack.Push(_converterLookup[systemIdentifier].ToDecimal(numberPart));
            }
            else
            {
                throw new ArgumentException($"Invalid token: {token}");
            }
        }
    }
}
