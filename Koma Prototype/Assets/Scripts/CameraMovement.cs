using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public Vector2 offset = new Vector2(2f, 1f);
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public Transform player;

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y + offset.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
   
    }
}
