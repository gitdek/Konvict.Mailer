﻿
using System;
using System.Collections.Generic;

namespace Injector.Utilities
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Transformer which takes an object of type <typeparamref name="S"/> and transforms it to type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="S">Source object to transform.</typeparam>
        /// <typeparam name="T">Target object of the transformation.</typeparam>
        public interface ITransformer<S, T>
        {
            /// <summary>
            /// Transforms <paramref name="source"/> into a result object.
            /// </summary>
            /// <param name="source">Object to transform.</param>
            /// <returns>Target object of the transformation.</returns>
            T Transform(S source);
        }

        /// <summary>
        /// Transforms all objects of the <paramref name="source"/> enumeration with the specified
        /// <paramref name="transformer"/>.
        /// </summary>
        /// <param name="sourceEnumeration">Source enumeration to be transformed.</param>
        /// <param name="transformer">Transformer to transform each object.</param>
        /// <returns>Enumeration containing the transformed objects.</returns>
        public static IEnumerable<T> Transform<S, T>(IEnumerable<S> sourceEnumeration, ITransformer<S, T> transformer)
        {
            foreach (S source in sourceEnumeration)
                yield return transformer.Transform(source);
        }

        /// <summary>
        /// Adds all elements in the <paramref name="source"/> enumeration to the <paramref name="target"/> collection.
        /// </summary>
        /// <typeparam name="S">Type of the source elements. Needs to be equal to <see cref="T"/> or to be a
        /// sub type.</typeparam>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="target">Target collection where all elements from <paramref name="source"/> will be added.</param>
        /// <param name="source">Source enumeration whose elements will be added to <paramref name="target"/>.</param>
        public static void AddAll<S, T>(ICollection<T> target, IEnumerable<S> source) where S : T
        {
            foreach (S s in source)
                target.Add(s);
        }

        /// <summary>
        /// Calculates the intersection of <paramref name="c1"/> and <paramref name="c2"/> and returns it.
        /// If the type parameters of the collections differ, the collection with the more general element type
        /// must be used at the second position.
        /// </summary>
        /// <typeparam name="S">Element type of the first source collection. May be more specific than
        /// the type parameter of the second collection.</typeparam>
        /// <typeparam name="T">Element type of the second source collection and the result collection.
        /// May be more general than the type parameter of the first collection <see cref="S"/>.</typeparam>
        /// <param name="c1">First source collection.</param>
        /// <param name="c2">Second source collection</param>
        /// <returns>Intersection of <paramref name="c1"/> and <paramref name="c2"/>.</returns>
        public static ICollection<T> Intersection<S, T>(ICollection<S> c1, ICollection<T> c2) where S : T
        {
            ICollection<T> result = new List<T>();
            foreach (S s in c1)
                if (c2.Contains(s))
                    result.Add(s);
            return result;
        }

        /// <summary>
        /// Compares two sets of elements with a reference type given by the two enumerations
        /// <paramref name="e1"/> and <paramref name="e2"/>. If the sets differ in size or in at least one
        /// element, the return value will be <c>false</c>, else it will be <c>true</c>.
        /// The comparison is based on the <see cref="object.Equals(object)"/> method of the objects in
        /// <paramref name="e1"/>. The comparison does NOT compare the ORDER of elements, so for two enumerations
        /// with the same items but different order, this method will return <c>true</c>.
        /// </summary>
        /// <typeparam name="S">Element type of the first enumeration.</typeparam>
        /// <typeparam name="T">Element type of the second enumeration.</typeparam>
        /// <param name="e1">First element enumeration to compare.</param>
        /// <param name="e2">Second element enumeration to compare.</param>
        /// <returns><c>true</c>, if the size and all elements of the enumerations are the same,
        /// else <c>false</c>. The order of elements doesn't matter in the enumerations.</returns>
        public static bool CompareObjectCollections<S, T>(IEnumerable<S> e1, IEnumerable<T> e2)
            where S : class
            where T : class, S
        {
            List<S> l1 = new List<S>(e1);
            List<T> l2 = new List<T>(e2);
            if (l1.Count != l2.Count)
                return false;
            l1.Sort();
            l2.Sort();
            for (int i = 0; i < l1.Count; i++)
                if (!ObjectUtils.ObjectsAreEqual(l1[i], l2[i]))
                    return false;
            return true;
        }

        private class ComparisonEqualityComparer<T> : IEqualityComparer<T>
        {
            protected readonly Comparison<T> _comparison;

            public ComparisonEqualityComparer(Comparison<T> comparison)
            {
                _comparison = comparison;
            }

            #region Implementation of IEqualityComparer<T>

            public bool Equals(T x, T y)
            {
                return _comparison(x, y) == 0;
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }

            #endregion
        }

        /// <summary>
        /// Compares two sets of elements with a reference type given by the two enumerations
        /// <paramref name="e1"/> and <paramref name="e2"/>. If the sets differ in size or in at least one
        /// element, the return value will be <c>false</c>, else it will be <c>true</c>.
        /// The comparison is based on the return value of the <paramref name="comparison"/> delegate.
        /// The comparison does NOT compare the ORDER of elements, so for two enumerations with the same items but
        /// different order, this method will return <c>true</c>.
        /// </summary>
        /// <typeparam name="S">Element type of the first enumeration.</typeparam>
        /// <typeparam name="T">Element type of the second enumeration.</typeparam>
        /// <param name="e1">First element enumeration to compare.</param>
        /// <param name="e2">Second element enumeration to compare.</param>
        /// <param name="comparison">Comparison method used to compare the objects.</param>
        /// <returns><c>true</c>, if the size and all elements of the enumerations are the same,
        /// else <c>false</c>. The order of elements doesn't matter in the enumerations.</returns>
        public static bool CompareObjectCollections<S, T>(IEnumerable<S> e1, IEnumerable<T> e2,
            Comparison<S> comparison)
            where S : class
            where T : class, S
        {
            List<S> l1 = new List<S>(e1);
            List<S> l2 = new List<S>(l1.Count);
            AddAll(l2, e2);
            if (l1.Count != l2.Count)
                return false;
            l1.Sort(comparison);
            l2.Sort(comparison);
            for (int i = 0; i < l1.Count; i++)
                if (!ObjectUtils.ObjectsAreEqual(l1[i], l2[i], new ComparisonEqualityComparer<S>(comparison)))
                    return false;
            return true;
        }

        /// <summary>
        /// Compares two sets of elements with a non-reference type (i.e. struct or enum) given by the two
        /// enumerations <paramref name="e1"/> and <paramref name="e2"/>. If the sets differ in size or in at
        /// least one element, the return value will be <c>false</c>, else it will be <c>true</c>.
        /// The comparison is based on the return value of the <paramref name="comparison"/> delegate.
        /// The comparison does NOT compare the ORDER of elements, so for two enumerations with the same items but
        /// different order, this method will return <c>true</c>.
        /// </summary>
        /// <typeparam name="T">Element type of the enumeration.</typeparam>
        /// <param name="e1">First element enumeration to compare.</param>
        /// <param name="e2">Second element enumeration to compare.</param>
        /// <param name="comparison">Comparison method used to compare the objects.</param>
        /// <returns><c>true</c>, if the size and all elements of the enumerations are the same,
        /// else <c>false</c>. The order of elements doesn't matter in the enumerations.</returns>
        public static bool CompareCollections<T>(IEnumerable<T> e1, IEnumerable<T> e2,
            Comparison<T> comparison)
            where T : struct
        {
            List<T> l1 = new List<T>(e1);
            List<T> l2 = new List<T>(e2);
            if (l1.Count != l2.Count)
                return false;
            l1.Sort(comparison);
            l2.Sort(comparison);
            for (int i = 0; i < l1.Count; i++)
                if (comparison(l1[i], l2[i]) != 0)
                    return false;
            return true;
        }

        /// <summary>
        /// Exchanges the items at index <paramref name="index1"/> and <paramref name="index2"/> in the specified
        /// <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">List whose items should be swapped.</param>
        /// <param name="index1">First index to exchange.</param>
        /// <param name="index2">Second index to exchange.</param>
        public static void Swap<T>(IList<T> list, int index1, int index2)
        {
            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }
    }
}