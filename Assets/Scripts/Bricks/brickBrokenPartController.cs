using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBrokenPartController : MonoBehaviour {

    //类型
    //1 - left
    //2 - right
    public short part_type;
    private void Start()
    {
        //调整重心
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        Transform centerofMass = transform.Find("centerofMass");
        rigidbody.centerOfMass = centerofMass.position;
    }

    private void Update()
    {
        //清理
        if (transform.position.y <= -5.6f)
        {
            Destroy(gameObject);
        }
    }

    public void throwout(Vector3 ballPosition)
    {
        //定义刚体和力
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2();

        //球从下面打
        if (ballPosition.y <= transform.position.y)
        {            
            //分情况向部件施加力
            switch (part_type)
            {
                case 1:
                    force = new Vector2(Random.Range(-135f,-65f),Random.Range(35f, 65f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
                case 2:
                    force = new Vector2(Random.Range(65f, 165f), Random.Range(35f, 65f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
            }
        } else
        {
            //球从上面打
            switch (part_type)
            {
                case 1:
                    force = new Vector2(Random.Range(-95f, -55f), Random.Range(-65f, -35f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
                case 2:
                    force = new Vector2(Random.Range(55f, 95f), Random.Range(-65f, -35f));
                    rigidbody.AddForceAtPosition(force, ballPosition);
                    break;
            }
        }
    }
}
