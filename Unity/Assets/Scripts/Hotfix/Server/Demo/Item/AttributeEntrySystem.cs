namespace ET.Server
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
        
        public static AttributeEntryProto ToMessage(this AttributeEntry self)
        {
            AttributeEntryProto attributeEntryProto = AttributeEntryProto.Create();
            attributeEntryProto.Id    = self.Id;
            attributeEntryProto.Key   = self.Key;
            attributeEntryProto.Value = self.Value;
            attributeEntryProto.EntryType = (int)self.Type;
            return attributeEntryProto;
        }
    }
}

