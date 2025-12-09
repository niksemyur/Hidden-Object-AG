using UnityEngine;

namespace HiddenObject.Configs
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "_Core/Configs/Items/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _uiSprite;
        public string Id => _id;
        public string DisplayName => _displayName;
        public Sprite UiSprite => _uiSprite;
    }
}
