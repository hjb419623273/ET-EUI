namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AdventureHpBarEvent_ShowHeadHpInfo : AEvent<Scene, ShowAdventureHpBar>
    {
        protected override async ETTask Run(Scene scene, ShowAdventureHpBar args)
        {
            args.Unit.GetComponent<HeadHpViewComponent>().SetVisible(args.isShow);
            args.Unit.GetComponent<HeadHpViewComponent>().SetHp();
            await ETTask.CompletedTask;
        }
    }
}

