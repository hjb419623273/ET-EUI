using System;
using ET.Client;

namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_StartProductionHandler: MessageLocationHandler<Unit,C2M_StartProduction,M2C_StartProduction>
    {
        protected override async ETTask Run(Unit unit, C2M_StartProduction request, M2C_StartProduction response)
        {
            if (!ForgeProductionConfigCategory.Instance.Contain(request.ConfigId))
            {
                response.Error = ErrorCode.ERR_MakeConfigNotExist;
                return;
            }

            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();
            
            //是否有空闲的制造队列
            if (!forgeComponent.IsExistFreeQueue())
            {
                response.Error = ErrorCode.ERR_NoMakeFreeQueue;
                return;
            }

            var config = ForgeProductionConfigCategory.Instance.Get(request.ConfigId);
            int materialCount = unit.GetComponent<NumericComponent>().GetAsInt(config.ConsumId);
            if (materialCount < config.ConsumeCount)
            {
                response.Error = ErrorCode.ERR_MakeConsumeError;
                return;
            }
            unit.GetComponent<NumericComponent>()[config.ConsumId] -= config.ConsumeCount;
            Production production    = forgeComponent.StartProduction(request.ConfigId);
            response.ProductionProto = production.ToMessage();
            
            await ETTask.CompletedTask;
        }
    }
}