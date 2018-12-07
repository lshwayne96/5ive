/// <summary>
/// Defines a method that a class implements for class instances
/// to store the data of an IRestorable object.
/// </summary>
public interface IData {
    /// <summary>
    /// Restores the <c>restorable</c> with this.
    /// </summary>
    /// <param name="restorable">Restorable.</param>
    void Restore(IRestorable restorable);
}
