using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_triangle_collision : particles_collision {

    public override void ballHit(Vector2 ballPosition)
    {
        if (ballPosition.y >= -4.4f)
        {
            Instantiate(particle_launcher, ballPosition, new Quaternion(0,0,0,0));
        }
    }
}
