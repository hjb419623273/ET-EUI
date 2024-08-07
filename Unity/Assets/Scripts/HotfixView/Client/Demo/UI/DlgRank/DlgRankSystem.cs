using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.RankUI)]
	public class RankRefreshUI : ATimer<DlgRank>
	{
		protected override void Run(DlgRank t)
        {
			t?.RefreshRank();
		}
	}

    [FriendOf(typeof(DlgRank))]
    [FriendOfAttribute(typeof(ET.Client.RankComponent))]
    [FriendOfAttribute(typeof(ET.RankInfo))]
    public static class DlgRankSystem
    {

        public static void RegisterUIEvent(this DlgRank self)
        {
            self.RegisterCloseEvent<DlgRank>(self.View.E_CloseButton, true);
            self.View.E_RankLoopVerticalScrollRect.AddItemRefreshListener(self.OsnRankItemLoopHandler);
        }

        public static void ShowWindow(this DlgRank self, Entity contextData = null)
        {
            self.RefreshRank().Coroutine();
            self.rankTime = self.Root().GetComponent<TimerComponent>().NewRepeatedTimer(7000, TimerInvokeType.RankUI, self);
        }

        public static void HideWindow(this DlgRank self)
        {
            self.scrollItemRanks.Clear();
            self.Root().GetComponent<TimerComponent>().Remove(ref self.rankTime);
        }

        public static async ETTask RefreshRank(this DlgRank self)
        {
            try
            {
                int errorCode = await RankHelper.GetRankInfo(self.Root());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }

                if (!self.Root().GetComponent<UIComponent>().IsWindowVisible(WindowID.WindowID_Rank))
                {
                    return;
                }

                int count = self.Root().GetComponent<RankComponent>().RankInfos.Count;
                self.AddUIScrollItems(ref self.scrollItemRanks, count);
                self.View.E_RankLoopVerticalScrollRect.SetVisible(true, count);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        public static void OsnRankItemLoopHandler(this DlgRank self, Transform transform, int index)
        {
            Scroll_Item_rank scrollItemRank = self.scrollItemRanks[index];
            scrollItemRank.BindTrans(transform);

            RankInfo rankInfo = self.Root().GetComponent<RankComponent>().RankInfos[index];
            scrollItemRank.E_NameText.text = rankInfo.Name;
            scrollItemRank.E_RankOrderText.text = (index + 1) + "";
            scrollItemRank.E_LevelText.text = rankInfo.Count + "";
        }
    }
}
