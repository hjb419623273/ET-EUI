using TrueSync;

namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class M2C_StartGameLevelHandler : MessageLocationHandler<Unit, C2M_StartGameLevel, M2C_StartGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_StartGameLevel request, M2C_StartGameLevel response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            //冒险状态
            if (numericComponent.GetAsInt(NumericType.AdventureState) != 0)
            {
                //冒险进行中
                response.Error = ErrorCode.ERR_AlreadyAdventureState;
                return;
            }

            //垂死状态
            if (numericComponent.GetAsInt(NumericType.DyingState) != 0)
            {
                response.Error = ErrorCode.ERR_AdventureInDying;
                return;
            }
            
            //关卡id是否在配置中
            if (!BattleLevelConfigCategory.Instance.Contain(request.LevelId))
            {
                response.Error = ErrorCode.ERR_AdventureErrorLevel;
                return;
            }
            
            //等级是否满足配置等级
            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(request.LevelId);
            if (numericComponent[NumericType.Level] < config.MiniEnterLevel[0])
            {
                response.Error = ErrorCode.ERR_AdventureLevelNotEnough;
            }
            
            //这里冒险状态如果 =0 代表不在冒险中 ！0 在冒险状态
            numericComponent.Set(NumericType.AdventureState, request.LevelId);
            
            //记录开始时间
            numericComponent.Set(NumericType.AdventureStartTime, TimeInfo.Instance.ServerNow());
            //设置本次战斗的随机种子 保证客户端的战斗中每次随机产生的数能在服务器端复现

            numericComponent.Set(NumericType.BattleRandomSeed, RandomGenerator.RandInt32());
            await ETTask.CompletedTask;
        }
    }
}
