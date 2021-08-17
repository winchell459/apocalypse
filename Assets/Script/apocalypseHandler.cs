using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apocalypseHandler : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private Player player;
    public Vector3 playerStartPos;
    public camera MainCamera;

    public void TerrainBuilt(Vector3 mapCenter)
    {
        playerStartPos = mapCenter;
        player = Instantiate(PlayerPrefab, playerStartPos + Vector3.up, Quaternion.identity).GetComponent<Player>();
        MainCamera.Setup(player.transform);
    }
    public void StartGame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
