using System;

namespace FrEee.Utility;

/// <summary>
/// Status for an operation.
/// </summary>
public class Status
{
	/// <summary>
	/// Any exception that may have occurred.
	/// </summary>
	public Exception Exception
	{
		get { return exception; }
		set
		{
			exception = value;
			if (Changed != null)
				Changed();
		}
	}

	/// <summary>
	/// Message indicating current sub-operation.
	/// </summary>
	public string Message
	{
		get { return message; }
		set
		{
			message = value;
			if (Changed != null)
				Changed();
		}
	}

	/// <summary>
	/// Progress to completion.
	/// 0 = just started.
	/// 1 = done.
	/// </summary>
	public double Progress
	{
		get { return progress; }
		set
		{
			progress = value;
			if (Changed != null)
				Changed();
		}
	}

	private Exception exception;
	private string message;
	private double progress;

	public event ChangedDelegate Changed;

	public delegate void ChangedDelegate();
}
