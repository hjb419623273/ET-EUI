namespace ET.Client
{
    public static partial class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene root, string sceneName, long sceneInstanceId)
        {
            root.RemoveComponent<AIComponent>();
            
            CurrentScenesComponent currentScenesComponent = root.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = CurrentSceneFactory.Create(sceneInstanceId, sceneName, currentScenesComponent);
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();
            currentScene.AddComponent<AdventureComponent>();
                    
            // 可以订阅这个事件中创建Loading界面 --> SceneChangeStart_AddComponent
            // 场景切换消息
            EventSystem.Instance.Publish(root, new SceneChangeStart());
            
            // 等待CreateMyUnit的消息 在M2M_UnitTransferRequestHandler中发送M2C_CreateMyUnit消息
            // 在M2C_CreateMyUnitHandler中通知Wait_CreateMyUnit
            Wait_CreateMyUnit waitCreateMyUnit = await root.GetComponent<ObjectWait>().Wait<Wait_CreateMyUnit>();
            M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;
            
            //创建客户端unit
            Unit unit = UnitFactory.Create(currentScene, m2CCreateMyUnit.Unit);
            unitComponent.Add(unit);
            root.RemoveComponent<AIComponent>();

            //这里为了显示加载界面延时了2s
            await root.GetComponent<TimerComponent>().WaitAsync(2000);
            
            EventSystem.Instance.Publish(currentScene, new SceneChangeFinish());
            // 通知等待场景切换的协程
            root.GetComponent<ObjectWait>().Notify(new Wait_SceneChangeFinish());
        }
    }
}