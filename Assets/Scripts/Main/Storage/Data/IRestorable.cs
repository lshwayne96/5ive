/// <summary>
/// Defines a method that a class implements for class instances
/// to be restorable from IData objects.
/// </summary>
public interface IRestorable {
    /// <summary>
    /// Restores this with <c>data></c>
    /// </summary>
    /// <param name="data">Data.</param>
    void RestoreWith(IData data);
}