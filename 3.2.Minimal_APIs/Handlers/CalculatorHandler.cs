public static class CalculatorHandler
{
    public static string Calculate(string operation, int a, int b, ICalculatorServices calculatorServices)
    {
        var result = calculatorServices.Calculate(a, b, operation);
        return result.ToString();
    }
}