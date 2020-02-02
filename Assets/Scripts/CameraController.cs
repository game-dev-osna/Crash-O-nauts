using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed;
    GameManager manager;
    List<Player> players;

    [SerializeField]
    private float minSize;
    [SerializeField]
    private float speed = 5.0f;

    //bounding box
    private float maxZ;
    private float minZ;
    private float maxY;
    private float minY;
    private Camera cam;


    // Start is called before the first frame update
    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        players = new List<Player>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        minY =  9999f;
        minZ =  9999f;
        maxY = -9999f;
        maxZ = -9999f;

        foreach(Player player in manager.players)
        {
            if(player.transform.position.y < minY)
            {
                minY = player.transform.position.y;
            }
            if(player.transform.position.y > maxY)
            {
                maxY = player.transform.position.y;
            }

            if (player.transform.position.z < minZ)
            {
                minZ = player.transform.position.z;
            }
            if (player.transform.position.z > maxZ)
            {
                maxZ = player.transform.position.z;
            }
        }

        Vector3 targetPos = new Vector3(transform.position.x, (maxY + minY) / 2f, (maxZ + minZ) / 2f);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        transform.RotateAround(Vector3.zero, new Vector3(1,0,0), Time.deltaTime * RotationSpeed);

        float size = minSize;
        if(size < maxY-minY + 1)
        {
            size = maxY - minY +1;
        }
        if(size < maxZ - minZ + 1)
        {
            size = maxZ - minZ +1;
        }
        if (manager.gamestate == GameManager.State.Travel)
            size = minSize * 4.0f;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.deltaTime * speed);
    }
}
