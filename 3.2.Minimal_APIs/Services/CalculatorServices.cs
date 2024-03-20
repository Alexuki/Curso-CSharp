public class CalculatorServices : ICalculatorServices
{
    private readonly ICalculationEngine _calculationEngine;
    private readonly ILogger<CalculatorServices> _logger;

    public CalculatorServices(ICalculationEngine calculationEngine, ILogger<CalculatorServices> logger)
    {
        _calculationEngine = calculationEngine;
        _logger = logger;
    }
    public int Calculate(int a, int b, string operation)
    {
        switch (operation)
        {
            case "add":
                return _calculationEngine.Add(a, b);
            case "sub":
                return _calculationEngine.Substract(a, b);
            case "mul":
                return _calculationEngine.Multiply(a, b);
            case "div":
                return _calculationEngine.Divide(a, b);
            default:
                var message = $"Operation '{operation}' not supported";
                _logger.LogError(message);
                throw new ArgumentOutOfRangeException(nameof(operation), message);
        }
    }
}