using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, -1f); //если движется вправо, то поворот
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); //если движется влево, то поворот
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
