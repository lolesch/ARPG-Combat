using System;
using System.Linq;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Container
{
    [Serializable]
    public abstract class Entity : ScriptableObject
    {
        [SerializeField] private string guid = string.Empty;
        [SerializeField] private Sprite icon;
        [TextArea(5, 20)]
        [SerializeField] private string description;

        public string GUID { get => guid; }
        public Sprite Icon { get => icon; }

        protected virtual void Awake()
        {
            if (GUID == string.Empty)
                EditorDebug.LogWarning($"Entity \t {name} has no guid OR is not in the 'AllEntitiesDictionary'! Use the ContextMenu to assign a new GUID");
        }

        [ContextMenu("Human Readable GUID")]
        private void HRGUID()
        {
            guid = string.Concat(String.Concat(name.Where(c => !Char.IsWhiteSpace(c))), "-", Guid.NewGuid().ToString());
        }
    }
}