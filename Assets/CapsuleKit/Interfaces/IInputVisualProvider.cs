using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IInputVisualProvider : IGameModule
    {
        public interface IInputVisualProvider : Capsule.Core.IGameModule
        {
            GameObject RequestVisual(Capsule.Core.Inputs input, string optionalKeyLabel = null);
        }
    }
}