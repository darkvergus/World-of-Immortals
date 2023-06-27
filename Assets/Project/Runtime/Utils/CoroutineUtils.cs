using UnityEngine;

namespace Utils
{
    public static class CoroutineUtils
    {
        public static readonly WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
    }
}