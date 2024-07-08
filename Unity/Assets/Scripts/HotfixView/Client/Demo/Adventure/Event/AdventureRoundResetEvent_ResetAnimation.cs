using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AdventureRoundResetEvent_ResetAnimation : AEvent<Scene, AdventureRoundReset>
    {
        protected override async ETTask Run(Scene scene, AdventureRoundReset args)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(scene);
            unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);
            await ETTask.CompletedTask;
        }
    }
}

