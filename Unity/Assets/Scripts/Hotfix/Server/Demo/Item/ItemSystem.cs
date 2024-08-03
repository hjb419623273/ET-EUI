namespace ET.Server
{
    [EntitySystemOf(typeof(Item))]
    [FriendOfAttribute(typeof(ET.Item))]
    public static partial class ItemSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Item self, int configID)
        {
            self.ConfigId = configID;
        }
        [EntitySystem]
        private static void Destroy(this ET.Item self)
        {
            self.Quality = 0;
            self.ConfigId = 0;
        }

        public static ItemInfo ToMessage(this Item self, bool isAllInfo = true)
        {
            ItemInfo itemInfo = ItemInfo.Create();
            itemInfo.ItemUid = self.Id;
            itemInfo.ItemConfigId = self.ConfigId;
            itemInfo.ItemQuality = self.Quality;
            if (!isAllInfo)
            {
                return itemInfo;
            }

            //装备词条
            EquipInfoComponent equipInfoComponent = self.GetComponent<EquipInfoComponent>();
            if (equipInfoComponent != null)
            {
                itemInfo.EquipInfo = equipInfoComponent.ToMessage();
            }
            else
            {
                //如果EquipInfoComponent组件未添加 在这个里添加下
                itemInfo.EquipInfo = self.AddComponent<EquipInfoComponent>().ToMessage();
            }

            return itemInfo;
        }
    }
}