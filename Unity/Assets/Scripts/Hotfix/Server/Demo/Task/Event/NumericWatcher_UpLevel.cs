namespace ET.Server
{
    [NumericWatcher(SceneType.Map,NumericType.Level)]
    public class NumericWatcher_UpLevel: INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.UpLevel,(int)args.New);
            
            RankHelper.AddOrUpdateLevelRank(unit);
        }
    }
}