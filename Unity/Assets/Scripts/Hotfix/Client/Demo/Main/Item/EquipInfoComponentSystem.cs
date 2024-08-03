namespace ET.Client
{
    [EntitySystemOf(typeof(EquipInfoComponent))]
    [FriendOfAttribute(typeof(ET.Client.EquipInfoComponent))]
    public static partial class EquipInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.EquipInfoComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.EquipInfoComponent self)
        {
            self.IsInited = false;
            self.Score    = 0;
            for (int i = 0; i < self.EntryList.Count; i++)
            {
                AttributeEntry ent = self.EntryList[i];
                ent?.Dispose();;
            }
            self.EntryList.Clear();
        }

        public static void FromMessage(this EquipInfoComponent self, EquipInfoProto equipInfoProto)
        {
            self.Score = equipInfoProto.Score;
            for (int i = 0; i < self.EntryList.Count; i++)
            {
                AttributeEntry ent = self.EntryList[i];
                ent?.Dispose();
            }
            self.EntryList.Clear();

            for (int i = 0; i < equipInfoProto.AttributeEntryProtoList.Count; i++)
            {
                AttributeEntry attributeEntry = self.AddChild<AttributeEntry>();
                attributeEntry.FromMessage(equipInfoProto.AttributeEntryProtoList[i]);
                self.EntryList.Add(attributeEntry);
            }
            self.IsInited = true;
        }
    }
}