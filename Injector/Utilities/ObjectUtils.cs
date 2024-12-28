using System.Collections.Generic;

namespace Injector.Utilities
{
  public static class ObjectUtils
  {
    /// <summary>
    /// Checks if the two objects <paramref name="o1"/> and <paramref name="o2"/> are equal.
    /// This method also copes with <c>null</c> values, i.e. if both objects are <c>null</c>, the
    /// method will return <c>true</c>.
    /// The return value is based on the <see cref="object.Equals(object)"/> method of
    /// <paramref name="o1"/>.
    /// </summary>
    /// <typeparam name="T">Type of the objects.</typeparam>
    /// <param name="o1">First object to compare.</param>
    /// <param name="o2">Second object to compare.</param>
    /// <returns><c>true</c>, if both objects are <c>null</c> or if <paramref name="o1"/> is equal
    /// to <paramref name="o2"/>, based on the result of the <see cref="object.Equals(object)"/> method
    /// of <paramref name="o1"/>.</returns>
    public static bool ObjectsAreEqual<T>(T o1, T o2)
        where T : class
    {
      if (o1 == null && o2 == null)
        return true;
      if (o1 == null || o2 == null)
        return false;
      return o1.Equals(o2);
    }

    /// <summary>
    /// Checks if the two objects <paramref name="o1"/> and <paramref name="o2"/> are equal based on
    /// a comparer.
    /// This method also copes with <c>null</c> values, i.e. if both objects are <c>null</c>, the
    /// method will return <c>true</c>.
    /// </summary>
    /// <typeparam name="T">Type of the objects.</typeparam>
    /// <param name="o1">First object to compare.</param>
    /// <param name="o2">Second object to compare.</param>
    /// <param name="comparer">Equality comparer method to compute object equality.</param>
    /// <returns><c>true</c>, if both objects are <c>null</c> or if <paramref name="o1"/> is equal
    /// to <paramref name="o2"/>, based on the result of the <see cref="IEqualityComparer{T}.Equals(T,T)"/>
    /// method.</returns>
    public static bool ObjectsAreEqual<T>(T o1, T o2, IEqualityComparer<T> comparer)
        where T : class
    {
      if (o1 == null && o2 == null)
        return true;
      if (o1 == null || o2 == null)
        return false;
      return comparer.Equals(o1, o2);
    }
  }
}