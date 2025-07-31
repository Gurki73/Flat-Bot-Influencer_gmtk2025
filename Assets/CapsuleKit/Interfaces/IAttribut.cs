using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IAttribute
    {
        string Id { get; }
        string Name { get; }
    }
}