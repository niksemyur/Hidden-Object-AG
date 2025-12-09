using HiddenObject.Configs;

namespace HiddenObject.Signals.Item
{
    public struct ItemClickedSignal
    {
        public string ItemId;
    }

    public struct ItemAssignedSignal
    {
        public ItemData GetItemData;
        public int SlotIndex;
    }

    public struct ItemFoundSignal
    {
        public string ItemId;
        public int SlotIndex;
    }
}