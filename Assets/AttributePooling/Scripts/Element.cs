using System;
using System.Collections.Generic;
using UnityEngine;


namespace AttributePool
{
    [Serializable]
    public class Element
    {
        public string Id;

        public string Title;
        [TextArea] public string Body;

        public GameObject payload;

        public List<AttributeValue> Attributes = new();

        public Element(string id, List<AttributeValue> attributes = null)
        {
            Id = id;
            Attributes = attributes ?? new();
        }

        public float GetAttribute(string name)
        {
            return Attributes.Find(a => a.Name == name)?.Value ?? 0f;
        }
    }
}
