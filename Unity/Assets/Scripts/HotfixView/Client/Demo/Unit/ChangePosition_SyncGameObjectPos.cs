using UnityEngine;
using UnityEngine.Rendering;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ChangePosition_SyncGameObjectPos: AEvent<Scene, ChangePosition>
    {
        protected override async ETTask Run(Scene scene, ChangePosition args)
        {
            Unit unit = args.Unit;
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }

            Transform transform = gameObjectComponent.Transform;
            transform.position = unit.Position;

            
            //根据y轴坐标进行一个排序
            SortingGroup sortingGroup = transform.GetComponent<SortingGroup>();
            if (sortingGroup == null)
            {
                return;
            }

            sortingGroup.sortingOrder = (int)-args.Unit.Position.y;
            await ETTask.CompletedTask;
        }
    }
}