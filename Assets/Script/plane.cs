using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    public GameObject GroundBlockPrefab;
    public float GroundBlockStepHeight = 0.3f;
    public float BlockSpacing = 1;
    public int width = 1000;
    public int height = 1000;

    private GameObject[,] Map;
    // Start is called before the first frame update
    void Start()
    {
        Map = new GameObject[width,height];
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void GenerateTerrain()
    {
        for (int h = 0; h < height; h += 1)
        {
            for (int w = 0; w < width; w += 1)
            {
                float y = .6f;
                if (w > 0 && h == 0)
                {
                    float leftY = Map[w - 1, h].transform.position.y;
                    y = GetRandomY(leftY, leftY);
                }
                else if (w > 0 && h > 0)
                {
                    float leftY = Map[w - 1, h].transform.position.y;
                    float downY = Map[w, h - 1].transform.position.y;
                    y = GetRandomY(downY, leftY);
                }
                else if (w == 00 && h > 0)
                {
                    float downY = Map[w, h - 1].transform.position.y;
                    y = GetRandomY(downY, downY);
                }
                else
                {

                }
                GameObject newBlock = Instantiate(GroundBlockPrefab, new Vector3(w, 0, h) * BlockSpacing + Vector3.up * y, Quaternion.identity);
                Map[w, h] = newBlock;
            }

        }
    }


    private float GetRandomY(float downY, float leftY)
    {
        float y = 0;
        float[] values = { -GroundBlockStepHeight, 0, GroundBlockStepHeight };

        if (downY - leftY == 0)
        {
            int choice = Random.Range(0, 3);
            float a = values[choice];
            y = downY + a;
            Debug.Log("downY == LeftY" + a);
        }
        else if (0 < downY - leftY && downY - leftY < 2 * GroundBlockStepHeight)
        {
            int choice = Random.Range(0, 3);
            y = choice == 0 ? downY : leftY;
        }
        else if (0 < leftY - downY && leftY - downY < 2 * GroundBlockStepHeight)
        {
            int choice = Random.Range(0, 3);
            y = choice == 0 ? downY : leftY;
        }
        else if (downY - leftY >= 2 * GroundBlockStepHeight)
        {
            y = downY - GroundBlockStepHeight;
        }
        else if (leftY - downY >= 2 * GroundBlockStepHeight)
        {
            y = leftY - GroundBlockStepHeight;
        }
        Debug.Log(y);
        return y;
    }
        
    }
