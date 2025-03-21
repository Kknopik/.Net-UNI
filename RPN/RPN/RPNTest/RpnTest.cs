namespace RPNTest;

[TestFixture]
public class RpnTest {
    private Rpn _sut;
    
    [SetUp]
    public void Setup() {
        _sut = new Rpn();
    }

    // Check setup  

    [Test]
    public void CheckIfTestWorks() {
        Assert.Pass();
    }

    [Test]
    public void CheckIfCanCreateSut() {
        Assert.That(_sut, Is.Not.Null);
    }

    // Check single number input

    [Test]
    public void SingleDigitOneInputOneReturn() {
        var result = _sut.EvaluateExpression("1");

        Assert.That(result, Is.EqualTo(1));

    }

    [Test]
    public void SingleDigitOtherThenOneInputNumberReturn() {
        var result = _sut.EvaluateExpression("2");

        Assert.That(result, Is.EqualTo(2));

    }

    [Test]
    public void TwoDigitsNumberInputNumberReturn() {
        var result = _sut.EvaluateExpression("12");

        Assert.That(result, Is.EqualTo(12));

    }

    [Test]
        public void TwoNumbersGivenWithoutOperator_ThrowsExcepton() {
            Assert.Throws<InvalidOperationException>(() => _sut.EvaluateExpression("1 2"));

        }

    [Test]
    public void OperatorPlus_AddingTwoNumbers_ReturnCorrectResult() {
        var result = _sut.EvaluateExpression("1 2 +");

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void OperatorTimes_AddingTwoNumbers_ReturnCorrectResult() {
        var result = _sut.EvaluateExpression("2 2 *");

        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    public void OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult() {
        var result = _sut.EvaluateExpression("2 1 -");

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void ComplexExpression() {
        var result = _sut.EvaluateExpression("15 7 1 1 + - / 3 * 2 1 1 + + -");

        Assert.That(result, Is.EqualTo(5));
    }

    //added

    //numberconverter
    [Test]
    public void BinaryNumber_ReturnsCorrectResult()
    {
        Assert.That(_sut.EvaluateExpression("B101111"), Is.EqualTo(47));
    }

    [Test]
    public void HexadecimalNumber_ReturnsCorrectResult()
    {
        Assert.That(_sut.EvaluateExpression("#A5"), Is.EqualTo(165));
    }

    [Test]
    public void DecimalNumber_ReturnsCorrectResult()
    {
        Assert.That(_sut.EvaluateExpression("25"), Is.EqualTo(25));
    }


    //operations
    [Test]
    public void OperatorMinus_SubstractingTwoNumbers_NegativeNumberResult() {
        var result = _sut.EvaluateExpression("7 18 -");

        Assert.That(result, Is.EqualTo(-11));
    }

    [Test]
    public void OperatorDivision_ReturnsCorrectResult()
    {
        Assert.That(_sut.EvaluateExpression("66 6 /"), Is.EqualTo(11));
    }

    [Test]
    public void OperatorDivision_ByZero_ThrowsException()
    {
        Assert.Throws<DivideByZeroException>(() => _sut.EvaluateExpression("1 0 /"));
    }

    [Test]
    public void OperatorFactorial_ReturnsCorrectResult()
    {
        Assert.That(_sut.EvaluateExpression("10 !"), Is.EqualTo(3628800));
    }

    [Test]
    public void OperatorFactorial_NegativeNumber_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("-3 !"));
    }


    //invalid data tests
    [Test]
    public void InvalidBinaryFormat_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("B102"));
    }

    [Test]
    public void InvalidHexadecimalFormat_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("#G1A"));
    }

    [Test]
    public void InvalidDecimalFormat_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("25.5"));
    }


    [Test]
    public void InvalidToken_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("b 1 +"));
    }

    [Test]
    public void InvalidExpression_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => _sut.EvaluateExpression("3 + 1 /"));
    }

    [Test]
    public void UnsupportedNumeralSystem_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.EvaluateExpression("C123"));
    }



}