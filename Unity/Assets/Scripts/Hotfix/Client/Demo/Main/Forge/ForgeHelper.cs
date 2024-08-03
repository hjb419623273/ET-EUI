using System;

namespace ET.Client
{
    public static class ForgeHelper
    {
        //请求开始产生物品
        public static async ETTask<int> StartProduction(Scene scene, int productionConfigId)
        {
            //检测配置中是否有该物品
            if (!ForgeProductionConfigCategory.Instance.Contain(productionConfigId))
            {
                return ErrorCode.ERR_MakeConfigNotExist;
            }
            
            //判定生产材料数量是否满足
            var config = ForgeProductionConfigCategory.Instance.Get(productionConfigId);
            int materailCount = UnitHelper.GetMyUnitNumericComponent(scene.CurrentScene()).GetAsInt(config.ConsumId);
            if (materailCount < config.ConsumeCount)
            {
                return ErrorCode.ERR_MakeConsumeError;
            }

            M2C_StartProduction m2CStartProduction = null;
            try
            {
                C2M_StartProduction c2MStartProduction = C2M_StartProduction.Create();
                c2MStartProduction.ConfigId = productionConfigId;
                m2CStartProduction = await scene.GetComponent<ClientSenderComponent>().Call(c2MStartProduction) as M2C_StartProduction;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CStartProduction.Error != ErrorCode.ERR_Success)
            {
                return m2CStartProduction.Error;
            }

            scene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(m2CStartProduction.ProductionProto);
            return ErrorCode.ERR_Success;
        }
        
        //请求获取生产好的物品
        public static async ETTask<int> ReceivedProductionItem(Scene scene, long productionId)
        {
            //背包已满
            if (scene.GetComponent<BagComponent>().IsMaxLoad())
            {
                return ErrorCode.ERR_BagMaxLoad;
            }
            
            M2C_ReceiveProduction m2CReciveProduction = null;
            
            try
            {
                C2M_ReceiveProduction c2MReceiveProduction = C2M_ReceiveProduction.Create();
                c2MReceiveProduction.ProducitonId = productionId;
                m2CReciveProduction = await scene.GetComponent<ClientSenderComponent>().Call(c2MReceiveProduction) as M2C_ReceiveProduction;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CReciveProduction.Error != ErrorCode.ERR_Success)
            {
                return m2CReciveProduction.Error;
            }
            
            scene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(m2CReciveProduction.ProductionProto);
            return ErrorCode.ERR_Success;
        }
    }
}