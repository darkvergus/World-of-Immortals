using System;

namespace Events
{
    [Serializable]
    public struct Void : IEquatable<Void>
    {
        public bool Equals(Void other)
        {
            throw new NotImplementedException();
        }
    }
}
