using System;

namespace FrEee.Utility
{
    /// <summary>
    /// Marks a type as safe to pass from the client to the server.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    internal sealed class ClientSafeAttribute : Attribute
    {
    }
}
