namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ShowDamageValueViewEvent_RefreshHp : AEvent<Scene, ShowDamageValueView>
    {
        protected override async ETTask Run(Scene scene, ShowDamageValueView args)
        {
            args.TargeUnit.GetComponent<HeadHpViewComponent>().SetHp();
            scene.GetComponent<FlyDamageValueViewComponent>().SpawnFlyDamage(args.TargeUnit.Position, args.DamamgeValue).Coroutine();

            bool isAlive = args.TargeUnit.isAlive();
            await scene.Root().GetComponent<TimerComponent>().WaitAsync(400);
            args.TargeUnit?.GetComponent<HeadHpViewComponent>()?.SetVisible(isAlive);
            await ETTask.CompletedTask;
        }
    }
}