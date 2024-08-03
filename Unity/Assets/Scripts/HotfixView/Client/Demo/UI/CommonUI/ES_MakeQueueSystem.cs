using System;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.ES_MakeQueue))]
    [FriendOfAttribute(typeof(ET.Production))]
    public static partial class ES_MakeQueueSystem
    {

        public static void Refresh(this ES_MakeQueue self, Production production)
        {
            if (production == null || !production.IsMakingState())
            {
                self.uiTransform.SetVisible(false);
                return;
            }

            self.uiTransform.SetVisible(true);

            int itemConfigId = ForgeProductionConfigCategory.Instance.Get(production.ConfigId).ItemConfigId;
            self.ES_EquipItem.RefreshShowItem(itemConfigId);
            bool isCanReceive = production.IsMakeTimeOver() && production.IsMakingState();
            self.E_MakeTimeText.SetText(production.GetRemainingTimeStr());
            self.E_LeaftTimeSlider.value = production.GetRemainTimeValue();

            self.E_LeaftTimeSlider.SetVisible(!isCanReceive);
            self.E_MakeTimeText.SetVisible(!isCanReceive);
            self.E_MakeTipText.SetVisible(!isCanReceive);
            self.E_MakeOverTipText.SetVisible(isCanReceive);
            self.E_ReceiveButton.SetVisible(isCanReceive);
            self.E_ReceiveButton.AddListenerAsync(() => { return self.OnReceiveButtonHandler(production.Id); });

        }

        public static async ETTask OnReceiveButtonHandler(this ES_MakeQueue self, long productionId)
        {
            try
            {
                int errorCode = await ForgeHelper.ReceivedProductionItem(self.Root(), productionId);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                EventSystem.Instance.PublishAsync(self.Root(), new RefreshMakeEqueue()).Coroutine();
                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}