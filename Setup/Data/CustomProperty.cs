namespace Setup.Data
{
    public class CustomProperty
    {
        public CustomProperty(string propertyName, string defaultValue)
        {
            PropertyName = propertyName;
            DefaultValue = defaultValue;
        }

        public string PropertyName { get; }

        public string DefaultValue { get; }
    }
}
