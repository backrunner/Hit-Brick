using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopController : MonoBehaviour {

    public static ArrayList shopItems;
    public static ArrayList soldoutImgs;    //sold out的img

    private void Awake()
    {
        //init
        shopItems = new ArrayList();
        soldoutImgs = new ArrayList();
    }

    private void Start()
    {
        //初始化三个商品
        shopItems.Add(new ShopItem("addlife", "增加初始生命1点", 5000, getShopItemStatus("addlife")));
        shopItems.Add(new ShopItem("addball","开局增加1个球",3500, getShopItemStatus("addball")));
        shopItems.Add(new ShopItem("magnet", "每局可触发1次的磁铁", 3500, getShopItemStatus("magnet")));
    }

    //刷新img的状态
    public static void refreshSoldout()
    {
        for (int i=0;i<soldoutImgs.Count && shopItems.Count == soldoutImgs.Count; i++)
        {
            Image img = (Image)soldoutImgs[i];
            ShopItem item = (ShopItem)shopItems[i];
            Color t = img.color;
            if (item.soldout)
            {
                t.a = 1;
            } else
            {
                t.a = 0;
            }
            img.color = t;
        }
    }

    //获取商品状态
    public static bool getShopItemStatus(string name)
    {
        return Save.getBool("shopitem_" + name + "_status");
    }

    //保存商品状态
    public static void saveShopItemStatus()
    {
        foreach(ShopItem item in shopItems)
        {
            Save.setBool("shopitem_" + item.name + "_status",item.soldout);
        }
    }
}
