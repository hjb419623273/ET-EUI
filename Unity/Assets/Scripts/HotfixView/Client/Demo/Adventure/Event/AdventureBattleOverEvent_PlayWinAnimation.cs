namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AdventureBattleOverEvent_PlayWinAnimation : AEvent<Scene, AdventureBattleOver>
    {
        protected override async ETTask Run(Scene scene, AdventureBattleOver args)
        {
            args.WinUnit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Win);
            await ETTask.CompletedTask;
        }
    }
}
