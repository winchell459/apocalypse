using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apocalypseHandler : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private Player player;
    public Vector3 playerStartPos;
    public camera MainCamera;
    public PoisonArea SaveArea;
    public float SaveRadius = 20;

    public void TerrainBuilt(Vector3 mapCenter)
    {
        playerStartPos = mapCenter;
        player = Instantiate(PlayerPrefab, playerStartPos + Vector3.up, Quaternion.identity).GetComponent<Player>();
        player.CameraRig = MainCamera.transform;
        MainCamera.Setup(player.transform);
        SaveArea = Instantiate(SaveArea, mapCenter, Quaternion.identity);
        SaveArea.Setup(mapCenter, SaveRadius);
    }
    public void StartGame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
