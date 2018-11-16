using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SocialMessenger.Enumerations
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; }
        public int Value { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected Enumeration(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }
            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);
            return typeMatches && valueMatches;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Value;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T Get<T>(string name) where T : Enumeration
        {
            var field = typeof(T).GetField
            (
                name,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase
            );

            return (T)field.GetValue(null);
        }

        //User defined conversion from Enumeration to String
        public static implicit operator string(Enumeration aEnum)
        {
            return aEnum.Name;
        }

        /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///  Value
        ///  Meaning
        ///  Less than zero
        ///  This instance precedes <paramref name="obj">obj</paramref> in the sort order.
        ///  Zero
        ///  This instance occurs in the same position in the sort order as <paramref name="obj">obj</paramref>.
        ///  Greater than zero
        ///  This instance follows <paramref name="obj">obj</paramref> in the sort order.
        /// </returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="obj">obj</paramref> is not the same type as this instance.</exception>
        public int CompareTo(object obj)
        {
            return Value.CompareTo(((Enumeration)obj).Value);
        }
    }
}