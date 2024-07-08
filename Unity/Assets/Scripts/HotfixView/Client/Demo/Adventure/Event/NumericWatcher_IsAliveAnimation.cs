namespace ET.Client
{
    [NumericWatcher(SceneType.Current, NumericType.IsAlive)]
    public class NumericWatcher_IsAliveAnimation : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (args.New == 0)
            {
                unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Die);
            }
            else
            {
                unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);
            }
        }
    }
}