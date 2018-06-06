using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward{

    public string type;
    public long coin;

    public Reward(string type,long coin=0)
    {
        this.type = type;
        this.coin = coin;
    }
    public Reward(long coin)
    {
        this.type = "unknown";
        this.coin = coin;
    }
}
