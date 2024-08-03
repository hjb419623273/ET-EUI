
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(ES_MakeQueue))]
	[FriendOfAttribute(typeof(ES_MakeQueue))]
	public static partial class ES_MakeQueueViewSystem 
	{
		[EntitySystem]
		private static void Awake(this ES_MakeQueue self,Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ES_MakeQueue self)
		{
			self.DestroyWidget();
		}
	}


}
