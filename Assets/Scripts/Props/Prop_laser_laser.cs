using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_laser_laser : MonoBehaviour {

    //每次的长宽变化量
    public float deltaHeight;
    public float deltaWidth;
    //加速量
    public float addSpeed;

    //renderer
    private SpriteRenderer render;
    //collider
    private BoxCollider2D collision;
    //size
    private Vector2 size;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        size = render.size;
    }
	
	void Update () {
        processSize();
	}

    void processSize()
    {
        if (size.y > 35)
        {
            size.x -= deltaWidth;
            if (size.x <= 0)
            {
                Destroy(gameObject);
            }            
        } else
        {
            size.y += deltaHeight;
            deltaHeight += addSpeed;
        }
        render.size = size;
        collision.size = new Vector2(collision.size.x, size.y);
        collision.offset = new Vector2(0, size.y / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bricks")
        {
            brickController ctrl = collision.gameObject.GetComponent<brickController>();
            ctrl.destroyBrick();
        }
    }
}
