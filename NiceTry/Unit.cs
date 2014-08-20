using System;

namespace NiceTry
{
    /// <summary>
    ///     Represents a type with a single value. This type is often used to denote the successful completion of a
    ///     void-returning method (C#) or a Sub procedure (Visual Basic).
    /// </summary>
    public struct Unit : IEquatable<Unit>
    {
        private static readonly Unit Instance = new Unit();

        public static Unit Type
        {
            get { return Instance; }
        }

        public bool Equals(Unit other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Unit;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "()";
        }

        public static bool operator ==(Unit first, Unit second)
        {
            return true;
        }

        public static bool operator !=(Unit first, Unit second)
        {
            return false;
        }
    }
}