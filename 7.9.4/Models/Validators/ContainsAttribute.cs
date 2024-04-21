using System.ComponentModel.DataAnnotations;

public class ContainsAttribute : ValidationAttribute
{
    private readonly string _subString;

    public ContainsAttribute(string subString)
    {
        _subString = subString;
    }

    public override bool IsValid(object value)
    {
        var str = value?.ToString();
        return str?.ToLower().Contains(_subString.ToLower()) ?? false;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorMessage, name, _subString);
        //return string.Format("El campo {0} debe contener la cadena {1}", name, _subString);
    }
}