using System;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// <br>Base class for car parts.</br>
    /// <br>Helps us keep car parts organized and reduces the need for multiple components.</br>
    /// </summary>
    [Serializable]
    public abstract class PartBase
    {
        public abstract float Update(float deltaTime, params object[] extraParameters);
    }
}
