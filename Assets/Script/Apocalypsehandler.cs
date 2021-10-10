using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apocalypsehandler : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private Player player;
    private Vector3 playerStartPos;
    public camera MainCamera;
    public PoisonArea SafeArea;
    public float SafeRadius = 20;

    public bool BuildWorld = true;
    public Vector3 mapCenter = new Vector3(20,5,20);

    public void TerrainBuilt(Vector3 mapCenter)
    {
        playerStartPos = mapCenter;
        player = Instantiate(PlayerPrefab, playerStartPos + Vector3.up, Quaternion.identity).GetComponent<Player>();
        player.CameraRig = MainCamera.transform;
        MainCamera.Setup(player.transform);
        SafeArea = Instantiate(SafeArea, mapCenter, Quaternion.identity);
        SafeArea.Setup(mapCenter, SafeRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!BuildWorld)
        {
            TerrainBuilt(mapCenter);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
