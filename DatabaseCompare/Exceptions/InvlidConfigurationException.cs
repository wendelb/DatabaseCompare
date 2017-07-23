using System;

namespace DatabaseCompare.Exceptions
{
    class InvlidConfigurationException: Exception
    {
        public InvlidConfigurationException(string property, string value): base(string.Format("Illegal value {0} for AppSetting {1}. Please refer to the documentation for valid settings", value, property))
        {
        }
    }
}
