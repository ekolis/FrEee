﻿using System;
using FrEee.Serialization.Stringifiers;
using FrEee.Serialization.Stringifiers;

namespace FrEee.Serialization.Stringifiers;

public interface IStringifier
{
	Type SupportedType { get; }
	string Stringify(object o);
	object Destringify(string s);
}

public interface IStringifier<T> : IStringifier
{
	string Stringify(T t);
	new T Destringify(string s);
}