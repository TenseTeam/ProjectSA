namespace VUDK.Generic.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class CalledByAnimationEventAttribute : Attribute
    {
        public string Description { get; }

        public CalledByAnimationEventAttribute(string description)
        {
            Description = description;
        }
    }
}
