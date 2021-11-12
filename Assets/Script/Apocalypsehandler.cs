using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apocalypsehandler : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private Player player;
    [SerializeField] private Animal animal;
    [SerializeField] private GameObject animalPrefab;
    private Vector3 playerStartPos;
    public camera MainCamera;
    public PoisonArea SafeArea;
    public float SafeRadius = 20;

    public bool BuildWorld = true;
    public Vector3 mapCenter = new Vector3(20,5,20);
    private bool gameover = false;
    public GameObject DeathScreen;
    public GameObject[] BorderBlocks;

    public void TerrainBuilt(Vector3 mapCenter)
    {
        playerStartPos = mapCenter;
        player = Instantiate(PlayerPrefab, playerStartPos + Vector3.up, Quaternion.identity).GetComponent<Player>();
        if (animal == null) animal = Instantiate(animalPrefab).GetComponent<Animal>();
        animal.Target = player.transform;
        player.CameraRig = MainCamera.transform;
        MainCamera.Setup(player.transform);
        SafeArea = Instantiate(SafeArea, mapCenter, Quaternion.identity);
        SafeArea.Setup(mapCenter, SafeRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        DeathScreen.SetActive(false);
        if (!BuildWorld)
        {
            TerrainBuilt(mapCenter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover && player.isDead)
        {
            gameover = true;
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void GameOverButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Warning Sheet");
        
    }
    public void BuildBorder()
    {
        for (int layer = 1; layer <= 3; layer += 1)
        {
            float y = (-1 + (float)layer) / 2;

            for (int x = 1 - layer; x <= SafeRadius * 2 + layer; x += 1)
            {
                int zTop = (int)SafeRadius * 2 + layer;
                int zBottom = 1 - layer;
                BuildRandomBorderBlock(x, y, zBottom);
                BuildRandomBorderBlock(x, y, zTop);
            }
            for (int z = 1 - layer + 1; z <= SafeRadius * 2 + layer - 1; z += 1)
            {
                int xRight = (int)SafeRadius * 2 + layer;
                int xLeft = 1 - layer;
                BuildRandomBorderBlock(xRight, y, z);
                BuildRandomBorderBlock(xLeft, y, z);
            }
        }
       
    }
    private void BuildRandomBorderBlock(int x, float y, int z)
    {
        int blockIndex = Random.Range(0, BorderBlocks.Length);
        Instantiate(BorderBlocks[blockIndex], new Vector3(x, y, z), Quaternion.identity);
    }
}
