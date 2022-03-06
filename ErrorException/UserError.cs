namespace ErrorExeption
{
    public class InvalidPropValue : Exception
    {
        public string errorMessage;
        public InvalidPropValue(string propName, int expectedMinLendth, int expectedMaxLength)
        {
            errorMessage = $"The {propName} should be more than {expectedMinLendth} and less then {expectedMaxLength}";
        }
        public InvalidPropValue(string propName, int expectedMinLength, int expectedMaxLength, string valueName)
        {
            errorMessage = $"The {valueName} of the {propName} should be more than {expectedMinLength} and less then {expectedMaxLength}";
        }
    }
}