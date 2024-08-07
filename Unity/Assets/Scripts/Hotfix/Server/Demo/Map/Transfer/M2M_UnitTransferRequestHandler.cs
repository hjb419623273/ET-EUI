using System;
using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    public class M2M_UnitTransferRequestHandler: MessageHandler<Scene, M2M_UnitTransferRequest, M2M_UnitTransferResponse>
    {
        protected override async ETTask Run(Scene scene, M2M_UnitTransferRequest request, M2M_UnitTransferResponse response)
        {
            //这里真正传送unit 将unit添加在Map上
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            Unit unit = MongoHelper.Deserialize<Unit>(request.Unit);

            unitComponent.AddChild(unit);
            unitComponent.Add(unit);

            unit.AddComponent<UnitDBSaveComponent>();
            
            foreach (byte[] bytes in request.Entitys)
            {
                Entity entity = MongoHelper.Deserialize<Entity>(bytes);
                unit.AddComponent(entity);
            }

            unit.AddComponent<MoveComponent>();
            unit.AddComponent<PathfindingComponent, string>(scene.Name);
            unit.Position = new float3(-10, 0, -10);

            unit.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.OrderedMessage);

            // 通知客户端开始切场景 
            //注意 这里并没有使用异步跟后面逻辑有关联
            M2C_StartSceneChange m2CStartSceneChange = M2C_StartSceneChange.Create();
            m2CStartSceneChange.SceneInstanceId = scene.InstanceId;
            m2CStartSceneChange.SceneName = scene.Name;
            MapMessageHelper.SendToClient(unit, m2CStartSceneChange);

            // 通知客户端创建My Unit
            M2C_CreateMyUnit m2CCreateUnits = M2C_CreateMyUnit.Create();
            m2CCreateUnits.Unit = UnitHelper.CreateUnitInfo(unit);  //转换UnitInfo
            MapMessageHelper.SendToClient(unit, m2CCreateUnits);

            // 通知客户端同步背包信息
            ItemUpdateNoticeHelper.SyncAllBagItem(unit);
            // 通知客户端同步装备信息
            ItemUpdateNoticeHelper.SyncAllEquipItems(unit);
            // 通知客户端同步打造信息
            ForgeHelper.SyncAllProduction(unit);
            // 通知客户端同步任务信息
            TaskNoticeHelper.SyncAllTaskInfo(unit);
            
            unit.AddComponent<NumericNoticeComponent>();
            unit.AddComponent<AdventureCheckComponent>();
            // 加入aoi
            //unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);

            // 解锁location，可以接收发给Unit的消息
            await scene.Root().GetComponent<LocationProxyComponent>().UnLock(LocationType.Unit, unit.Id, request.OldActorId, unit.GetActorId());
        }
    }
}