namespace Routing
{
    public class CalculatorServices : ICalculatorServices
    {
        public int Multiply(int a, int b) => a * b;
    }

    public interface ICalculatorServices
    {
        int Multiply(int a, int b);
    }
}