namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SceneChangeFinish_StartAdventure : AEvent<Scene, SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, SceneChangeFinish args)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(scene);

            if (unit.GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState) == 0)
            {
                return;
            }

            await scene.Root().GetComponent<TimerComponent>().WaitAsync(3000);
            scene.GetComponent<AdventureComponent>().StartAdventure().Coroutine();
            await ETTask.CompletedTask;
        }
    }
}