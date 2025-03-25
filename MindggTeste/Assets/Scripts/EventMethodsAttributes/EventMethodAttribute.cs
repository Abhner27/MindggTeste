using System;

[AttributeUsage(AttributeTargets.Method)]
public class EventMethodAttribute : Attribute
{
    public EventType Type { get; }
    public EventMethodAttribute(EventType type)
    {
        Type = type;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class InitializeMethodAttribute : Attribute
{

}