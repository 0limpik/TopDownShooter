using System;

namespace TopDown.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class InfoAttribute : Attribute
    {
        public readonly string description;

        public InfoAttribute(string description)
        {
            this.description = description;
        }
    }
}
