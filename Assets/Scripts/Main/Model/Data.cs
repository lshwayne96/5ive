using System;

/// <summary>
/// A class that is meant to be extended by classes that intend
/// to act as <c>Data</c> classes.
/// </summary>
[Serializable]
public abstract class Data {

	/// <summary>
	/// Restore the specified restorable.
	/// </summary>
	/// <param name="restorable">Restorable.</param>
	public abstract void Restore(RestorableMonoBehaviour restorable);
}
