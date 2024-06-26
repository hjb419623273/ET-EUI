
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(ES_AttributeItem))]
	[FriendOfAttribute(typeof(ES_AttributeItem))]
	public static partial class ES_AttributeItemViewSystem 
	{
		[EntitySystem]
		private static void Awake(this ES_AttributeItem self,Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ES_AttributeItem self)
		{
			self.DestroyWidget();
		}
	}


}
