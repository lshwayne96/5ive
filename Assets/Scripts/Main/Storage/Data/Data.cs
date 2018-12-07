/// <summary>
/// A class that is meant to be extended by classes that intend
/// to act as <c>Data</c> classes.
/// </summary>
public abstract class Data {

	/// <summary>
	/// Restores the <c>restorable</c> with this.
	/// </summary>
	/// <param name="restorable">Restorable.</param>
	public abstract void Restore(IRestorable restorable);
}
