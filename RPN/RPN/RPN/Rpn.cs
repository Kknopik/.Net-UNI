﻿using System;
using System.Collections.Generic;
using RPN.Operations;
using RPN.Converters;
using System.ComponentModel;


namespace RPN;

public class Rpn
{
    private readonly RpnEvaluator _evaluator;
    private readonly Dictionary<string, InterfaceOperation> _operations;
    private readonly Dictionary<string, InterfaceConvertersConverter> _Converters;

    public Rpn()
    {
        var factory = new InstanceFactory();
        _operations = factory.CreateDictionary<InterfaceOperation>();
        _Converters = factory.CreateDictionary<InterfaceConvertersConverter>();
        _evaluator = new RpnEvaluator(_operations, _Converters);
    }


    public int EvaluateExpression(string input)
    {
        return _evaluator.Evaluate(input);
    }

    public bool AddKey(InterfaceOperation newKey)
    {
        _operations[newKey.Symbol] = newKey;
        return true;
    }

    public bool AddKey(InterfaceConvertersConverter newKey)
    {
        _Converters[newKey.Symbol] = newKey;
        return true;
    }

}