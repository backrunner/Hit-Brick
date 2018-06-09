using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem{
    public string name;
    public string desc;
    public long price;
    public bool soldout;

    public ShopItem(string name,string desc,long price)
    {
        this.name = name;
        this.desc = desc;
        this.price = price;
        soldout = false;
    }
    public ShopItem(string name, string desc, long price,bool soldout)
    {
        this.name = name;
        this.desc = desc;
        this.price = price;
        this.soldout = soldout;
    }

    public ShopItem()
    {
        this.name = "";
        this.desc = "";
        this.price = 0;
        soldout = false;
    }

    public void sold()
    {
        soldout = true;
        shopController.refreshSoldout();
    }
}
