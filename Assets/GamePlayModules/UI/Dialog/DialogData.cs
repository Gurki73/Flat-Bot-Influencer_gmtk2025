using System;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Talk
{
    [CreateAssetMenu(fileName = "NewDialog", menuName = "Talk/DialogData")]
    public class DialogData : ScriptableObject
    {
        public List<DialogLine> Lines;
    }

    [Serializable]
    public class DialogLine
    {
        public string Speaker; // "Player", "AI", etc.
        [TextArea(2, 4)]
        public string Text;
        public bool WaitForInput = true;

        public bool HasChoices => Choices != null && Choices.Count > 0;
        public List<DialogChoice> Choices;
    }
}
