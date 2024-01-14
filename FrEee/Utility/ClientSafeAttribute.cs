using System;

namespace FrEee.Utility;

/// <summary>
/// Marks a type as safe to pass from the client to the server.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
[Obsolete("ClientSafeAttribute is obsolete; please implement IPromotable instead.")]
internal sealed class ClientSafeAttribute : Attribute
{
}