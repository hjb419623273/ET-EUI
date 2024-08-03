namespace ET.Client
{
    [EntitySystemOf(typeof(AttributeEntry))]
    [FriendOfAttribute(typeof(ET.AttributeEntry))]
    public static partial class AttributeEntrySystem
    {
        [EntitySystem]
        private static void Awake(this ET.AttributeEntry self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.AttributeEntry self)
        {
            self.Key = 0;
            self.Value = 0;
            self.Type = EntryType.Common;
        }
        
        public static void FromMessage(this AttributeEntry self, AttributeEntryProto attributeEntryProto)
        {
            self.Key = attributeEntryProto.Key;
            self.Value = attributeEntryProto.Value;
            self.Type = (EntryType)attributeEntryProto.EntryType;
        }
    }
}