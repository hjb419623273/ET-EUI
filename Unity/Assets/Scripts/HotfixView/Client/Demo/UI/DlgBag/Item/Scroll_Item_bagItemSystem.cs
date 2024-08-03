namespace ET.Client
{
    [EntitySystemOf(typeof(Scroll_Item_bagItem))]
    public static partial class Scroll_Item_bagItemSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.Scroll_Item_bagItem self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.Scroll_Item_bagItem self)
        {

        }
        
        public static void Refresh(this Scroll_Item_bagItem self, long id)
        {
            Item item = self.Root().GetComponent<BagComponent>().GetItemById(id);

            self.E_IconImage.overrideSprite = IconHelper.LoadIconSprite(self.Root(), "Icons", item.Config.Icon);
            self.E_QualityImage.color = item.ItemQualityColor();
            //背包物品注册点击事件
            self.E_SelectButton.AddListenerWithId(self.OnShowItemEntryPopUpHandler, id);
        }

        public static void OnShowItemEntryPopUpHandler(this Scroll_Item_bagItem self, long Id)
        {
            self.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemPopUp);
            Item item = self.Root().GetComponent<BagComponent>().GetItemById(Id);
            //self.Root().GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>()?.RefreshInfo(item,ItemContainerType.Bag);
            
            //这里会产生环形引用 暂时用抛事件方式处理 感觉UI中环形引用避免不了 解决方案思考1.UI中相互调用都使用抛事件？
            EventSystem.Instance.PublishAsync(self.Root(), new RefreshItemPopUp(){
                Item = item,
                ItemContainerType = ItemContainerType.Bag
            }).Coroutine();
        }
    }
}
