using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            // Unit View层
            string assetsName = $"Assets/Bundles/Unit/Unit.prefab";
            GameObject bundleGameObject = await scene.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            UnitConfig config = unit.Config();
            Log.Info("AfterUnitCreate_CreateUnitView PrefabName : " + unit.Config().PrefabName);
            
            GameObject prefab = bundleGameObject.Get<GameObject>(unit.Config().PrefabName);

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();
            GameObject go = UnityEngine.Object.Instantiate(prefab, globalComponent.Unit, true);
            go.transform.position = unit.Position;
            unit.AddComponent<GameObjectComponent>().GameObject = go;
            unit.AddComponent<AnimatorComponent>();
            unit.AddComponent<HeadHpViewComponent>();

            args.Unit.Position = unit.Type() == UnitType.Player? new Vector3(-1.5f, 0, 0) : new Vector3(1.5f, RandomGenerator.RandomNumber(-1, 1), 0);

            await ETTask.CompletedTask;
        }
    }
}