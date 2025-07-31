using UnityEngine;

namespace Talk
{
    [System.Serializable]
    public struct DialogChoice
    {
        public string Label;  // What the player sees on the button
        public string Value;  // The internal value passed to logic

        public DialogChoice(string label, string value)
        {
            Label = label;
            Value = value;
        }
    }
}

