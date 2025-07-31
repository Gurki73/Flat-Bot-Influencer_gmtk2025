using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IAttributePool : IGameModule
    {
        List<GameObject> GetElementByAttributes(List<IAttribute> attributes, int count);
    }
}


