using System.Timers;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AdventureBattleRoundView_PlayAnimation : AEvent<Scene, AdventureBattleRoundView>
    {
        protected override async ETTask Run(Scene scene, AdventureBattleRoundView args)
        {
            if (!args.AttackUnit.isAlive() || !args.TargeUnit.isAlive())
            {
                return;
            }
            
            args.AttackUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Attack);
            args.TargeUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Hurt);

            long instanceId = args.TargeUnit.InstanceId;
            
            args.TargeUnit.GetComponent<GameObjectComponent>().GameObject.GetComponent<SpriteRenderer>().color = Color.red;
            await scene.Root().GetComponent<TimerComponent>().WaitAsync(300);
            if (instanceId != args.TargeUnit.InstanceId)
            {
                return;
            }
            args.TargeUnit.GetComponent<GameObjectComponent>().GameObject.GetComponent<SpriteRenderer>().color = Color.white;

            await ETTask.CompletedTask;
        }
    }
}

