namespace ET.Client
{
    [EntitySystemOf(typeof(Item))]
    [FriendOfAttribute(typeof(ET.Item))]
    public static partial class ItemSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Item self, int ConfigId)
        {
            self.ConfigId = ConfigId;
        }
        [EntitySystem]
        private static void Destroy(this ET.Item self)
        {
            self.Quality = 0;
            self.ConfigId = 0;
        }

        public static void FromMessage(this Item self, ItemInfo itemInfo)
        {
            //self.Id = itemInfo.ItemUid;
            self.ConfigId = itemInfo.ItemConfigId;
            self.Quality = itemInfo.ItemQuality;
            if (itemInfo.EquipInfo != null)
            {
                EquipInfoComponent equipInfoComponent = self.GetComponent<EquipInfoComponent>();
                if (equipInfoComponent == null)
                {
                    equipInfoComponent = self.AddComponent<EquipInfoComponent>();
                }
                equipInfoComponent.FromMessage(itemInfo.EquipInfo);
            }
        }
    }
}