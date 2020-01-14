namespace Setup.Data
{
    public class CustomProperty
    {
        public CustomProperty(string configPropertyName, string propertyName, string defaultValue)
        {
            PropertyName = propertyName;
            DefaultValue = defaultValue;
            ConfigPropertyName = configPropertyName;
        }

        public string ConfigPropertyName { get; }

        public string PropertyName { get; }

        public string DefaultValue { get; }
    }
}
