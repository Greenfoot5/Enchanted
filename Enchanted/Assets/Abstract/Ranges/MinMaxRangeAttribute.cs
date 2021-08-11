using System;

/// <summary>
/// An Attribute that works like Range, but we can use it on our own types
/// </summary>
public class MinMaxRangeAttribute : Attribute
{
	/// <summary>
	/// The constructor for the Attribute
	/// </summary>
	/// <param name="min">The minimum value for the variable</param>
	/// <param name="max">The maximum value for the variable</param>
	public MinMaxRangeAttribute(float min, float max)
	{
		Min = min;
		Max = max;
	}
	public float Min { get; }
	public float Max { get; }
}