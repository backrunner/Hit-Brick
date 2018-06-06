using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim_ctrl_clear : MonoBehaviour {

    public ArrayList rewardList;

    public GameObject txt_coinreward;
    private Reward current;

    private void Awake()
    {
        //init
        rewardList = new ArrayList();
    }

    public void doList()
    {
        if (rewardList.Count > 0)
        {
            Reward reward = (Reward)rewardList[0];
            current = reward;   //记录current
            GameObject obj = Instantiate(txt_coinreward, transform);
            Text txt = obj.GetComponent<Text>();
            anim_ctrl_coinreward ctrl = obj.GetComponent<anim_ctrl_coinreward>();
            ctrl.addNumber = reward.coin;
            switch (reward.type)
            {
                case "base":
                    txt.text = "基础奖励        +"+reward.coin.ToString();
                    break;
                case "random":
                    txt.text = "随机奖励        +" + reward.coin.ToString();
                    break;
                case "time":
                    txt.text = "时间奖励        +" + reward.coin.ToString();
                    break;
            }
        }
        else
        {
            //重设coin确保正确
            playerController.coin = playerController.targetcoin;
            playerController.saveData();
        }
    }

    public void removeCurrent()
    {
        if (current != null)
        {
            rewardList.Remove(current);
            current = null;
        }
    }

    public void addReward(string type,long coin)
    {
        Reward reward = new Reward(type, coin);
        rewardList.Add(reward);
    }
}
