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
        shopItems.Add(new ShopItem("addlife", "增加初始生命1点", 5000));
        shopItems.Add(new ShopItem("addball","开局增加1个球",3500));
        shopItems.Add(new ShopItem("magnet", "可触发1次的磁铁", 3500));
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
}
