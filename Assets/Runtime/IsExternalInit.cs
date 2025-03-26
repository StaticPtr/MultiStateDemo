namespace System.Runtime.CompilerServices
{
    // Used to make the "init" keyword work in Unity
    [AttributeUsage(AttributeTargets.All)]
    public class IsExternalInit : Attribute
    {
    }
}