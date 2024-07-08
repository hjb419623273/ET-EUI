using System;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AdventureBattleReportEvent_RequestEndGameLevel  : AEvent<Scene, AdventureBattleReport>
    {
        protected override async ETTask Run(Scene scene, AdventureBattleReport args)
        {
            if (args.BattleRoundResult == BattleRoundResult.KeepBattle)
            {
                return;
            }

            int errorCode = await AdventureHelper.RequestEndGameLevel(scene, args.BattleRoundResult, args.Round);
            if (errorCode != ErrorCode.ERR_Success)
            {
                return;
            }

            await scene.Root().GetComponent<TimerComponent>().WaitAsync(3000);
            
            scene.GetComponent<AdventureComponent>()?.ShowAdventureHpBarInfo(false);
            scene.GetComponent<AdventureComponent>()?.ResetAdventure();

            await ETTask.CompletedTask;
        }
    }
}