namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_EndGameLevelHandler : MessageLocationHandler<Unit, C2M_EndGameLevel, M2C_EndGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_EndGameLevel request, M2C_EndGameLevel response)
        {
            //检测关卡是否正常
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int levelId = numericComponent.GetAsInt(NumericType.AdventureState);
            if (levelId == 0 || !BattleLevelConfigCategory.Instance.Contain(levelId))
            {
                response.Error = ErrorCode.ERR_AdventureLevelIdError;
                return;
            }
            
            //检测上传的回合数是否正常
            if (request.Round <= 0)
            {
                response.Error = ErrorCode.ERR_AdventureResultError;
                return;
            }
            
            //战斗失败直接进入锤死状态
            if (request.BattleResult == (int)BattleRoundResult.LoseBattle)
            {
                numericComponent.Set(NumericType.DyingState, 1);
                numericComponent.Set(NumericType.AdventureState, 0);
                return;
            }

            if (request.BattleResult != (int)BattleRoundResult.WinBattle)
            {
                response.Error = ErrorCode.ERR_AdventureResultError;
                return;
            }
            
            //检测战斗结果是否正常
            if (! unit.GetComponent<AdventureCheckComponent>().CheckBattleWinResult(request.Round))
            {
                response.Error = ErrorCode.ERR_AdventureWinResultError;
                return;
            }
            
            numericComponent.Set(NumericType.AdventureState, 0);
            
            //战斗胜利增加经验值
            numericComponent[NumericType.Exp] += BattleLevelConfigCategory.Instance.Get(levelId).RewardExp;
            
            //下发闯关成功的奖励
            await ETTask.CompletedTask;
        }
    }
}

