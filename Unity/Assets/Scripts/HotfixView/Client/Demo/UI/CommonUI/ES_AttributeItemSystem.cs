using System;

namespace ET.Client
{
    public static partial class ES_AttributeItemSystem
    {
        public static void Refresh(this ES_AttributeItem self, int numbericType)
        {
            self.EAttributeValueText.text = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene())
                    .GetComponent<NumericComponent>().GetAsLong(numbericType).ToString();
        }

        public static void RegisterEvent(this ES_AttributeItem self, int numbericType)
        {
            self.E_AddButton.AddListenerAsync(() => { return self.RequestAddAtrribute(numbericType); });
        }

        public static async ETTask RequestAddAtrribute(this ES_AttributeItem self, int numericType)
        {
            try
            {
                //向服务器发送一条属性加点的消息
                int errorCode = await NumericHelper.RequestAddAttributePoint(self.Root(), numericType);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
                Log.Debug("加点成功！！！");
                EventSystem.Instance.PublishAsync(self.Root(), new RefreshRoleInfo()).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
            await ETTask.CompletedTask;
        }
    }
}