using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Autosalon.Infrastructure;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(false, "Field is required.")
            : new ValidationResult(true, "Field is qqq.");
    }
}

public class EmptyStringValidationRule : ValidationRule
{
    private string _errorMessage;

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value.ToString();

        if (input == String.Empty)
        {
            return new ValidationResult(false, this.ErrorMessage);
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}

public class PositiveIntValidationRule : ValidationRule
{
    private string _errorMessage;

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value.ToString();

        bool rt = Regex.IsMatch(input, @"^[1-9]\d*$");
        if (!rt)
        {
            return new ValidationResult(false, this.ErrorMessage);
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}

public class FloatValueValidationRule : ValidationRule
{
    private string _errorMessage;

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value.ToString();

        bool rt = Regex.IsMatch(input, @"^\d+([.,]\d{1,2})?$");
        if (!rt)
        {
            return new ValidationResult(false, this.ErrorMessage);
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}