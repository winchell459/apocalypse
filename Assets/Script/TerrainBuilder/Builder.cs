using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Clingo;

namespace TerrainBuilder
{
    public class Builder : MonoBehaviour
    {
        public ClingoSolver Solver;
        public GameObject BlockPrefab, GrassPrefab, SandPrefab, WaterPrefab, FoodPrefab;

        public int width = 10, depth = 10, height = 10, worldWidth = 2, worldDepth = 2;
        private GameObject[,] blocks;

        string aspCode = @"
            
            #const min_width = 1.
            #const min_depth = 1.
            #const min_height = 1.
            #const max_width = 50.
            #const max_depth = 50.
            #const max_height = 10.

            width(min_width..max_width).
            depth(min_depth..max_depth).
            height(min_height..max_height).
            block_types(grass;water;sand).

            three(-1;0;1).
            two(-1;1).
            1{block(XX,YY,ZZ,Type): height(YY), block_types(Type)}1 :- width(XX), depth(ZZ).
            :- block(XX,YY,ZZ,_), block(XX+Offset, Y2, ZZ,_), YY > Y2 + 1, two(Offset).
            :- block(XX,YY,ZZ,_), block(XX, Y2, ZZ+Offset,_), YY > Y2 + 1, two(Offset).

            %water must be adjacent to at least 2 water blocks
            :- block(XX,_,ZZ, water), Count = {
                                        block(XX-1,_,ZZ,water);
                                        block(XX+1,_,ZZ,water);
                                        block(XX,_,ZZ-1,water);
                                        block(XX,_,ZZ+1,water)
                                        }, Count < 2,
                                        XX > min_width, XX <= max_width, ZZ > min_depth, ZZ <= max_depth.

            %water must be same height as adjecent water
            :- block(XX,YY,ZZ,water), block(XX-1, Y2, ZZ,water), YY != Y2.
            :- block(XX,YY,ZZ,water), block(XX+1, Y2, ZZ,water), YY != Y2.
            :- block(XX,YY,ZZ,water), block(XX, Y2, ZZ-1,water), YY != Y2.
            :- block(XX,YY,ZZ,water), block(XX, Y2, ZZ+1,water), YY != Y2.
            
            %water must be one block lower than a none water block
            :- block(XX,YY,ZZ,water), block(XX+Offset, Y2, ZZ, Type), Type != water, not YY < Y2, two(Offset).
            :- block(XX,YY,ZZ,water), block(XX, Y2, ZZ+Offset, Type), Type != water, not YY < Y2, two(Offset).

            :- Count = {block(_,_,_,Type)}, block_types(Type), Count == 0.

            %:- not block(_,min_height,_,_).
            %:- not block(_,max_height,_,_).


            %sand cannot be surrounded by grass
            :- block(XX,_,ZZ,sand), {block(XX-1,_,ZZ,grass); block(XX+1,_,ZZ,grass);block(XX,_,ZZ-1,grass);block(XX,_,ZZ+1,grass)}==4, XX > min_width, XX <= max_width, ZZ > min_depth, ZZ <= max_depth.

            %grass cannot be surrounded by sand
            :- block(XX,_,ZZ,grass), {block(XX-1,_,ZZ,sand); block(XX+1,_,ZZ,sand)}==2.
            :- block(XX,_,ZZ,grass), {block(XX,_,ZZ-1,sand);block(XX,_,ZZ+1,sand)}==2.
            
            %sand must have a water or sand neighbor
            sand_depth(1..3).
            :- block(XX,Y1,ZZ,sand), {block(XX-Depth,_,ZZ, water): sand_depth(Depth);
                                       
                                        block(XX+Depth,_,ZZ,water): sand_depth(Depth);
                                        block(XX,_,ZZ-Depth,water): sand_depth(Depth);
                                        block(XX,_,ZZ+Depth,water): sand_depth(Depth)} < 1,
                                        XX > min_depth, XX <= max_width, ZZ > min_depth, ZZ <= max_depth.

            %neghboring waters must not be grass
            :- block(XX,Y1,ZZ,water), block(XX-1,Y2,ZZ,grass).
            :- block(XX,Y1,ZZ,water), block(XX+1,Y2,ZZ,grass).
            :- block(XX,Y1,ZZ,water), block(XX,Y2,ZZ-1,grass).
            :- block(XX,Y1,ZZ,water), block(XX,Y2,ZZ+1,grass).

            :- block(XX,Y1,ZZ,water), block(XX-1,Y2,ZZ-1,grass).
            :- block(XX,Y1,ZZ,water), block(XX-1,Y2,ZZ+1,grass).

            :- block(XX,Y1,ZZ,water), block(XX+1,Y2,ZZ-1,grass).
            :- block(XX,Y1,ZZ,water), block(XX+1,Y2,ZZ+1,grass).

        ";

        [SerializeField] private List<int> buildQueue = new List<int>();
        [SerializeField] private List<int> solvedAreas = new List<int>();
        // Start is called before the first frame update
        void Start()
        {
            StartBuild();

        }

        void StartBuild()
        {
            blocks = new GameObject[worldWidth * width + 2, worldDepth * depth + 2];
            for (int i = 1; i <= worldWidth * worldDepth; i += 1)
            {
                buildQueue.Add(i);
            }
        }

        void BuildArea(int maxWidth, int minWidth, int maxHeight, int minHeight, int maxDepth, int minDepth)
        {
            string boarderCode = GetBoarder(maxWidth, minWidth, maxDepth, minDepth);
            ClingoSolve(aspCode + boarderCode, $" -c max_width={maxWidth} -c max_height={maxHeight} -c max_depth={maxDepth} -c min_width={minWidth} -c min_height={minHeight} -c min_depth={minDepth} ");
        }

        void RemoveArea(int id)
        {
            Vector2 coordinates = GetCoordinate(id, worldWidth);
            int minWidth = ((int)coordinates.x - 1) * width + 1;
            int minDepth = ((int)coordinates.y - 1) * depth + 1;
            int maxWidth = ((int)coordinates.x - 1) * width + width;
            int maxDepth = ((int)coordinates.y - 1) * depth + depth;
            solvedAreas.Remove(id);
            for (int i = minWidth; i <= maxWidth; i += 1)
            {
                for (int j = minDepth; j <= maxDepth; j += 1)
                {

                    Destroy(blocks[i, j]);
                    blocks[i, j] = null;
                }
            }
        }

        string GetBoarder(int maxWidth, int minWidth, int maxDepth, int minDepth)
        {
            string code = "";
            for (int i = minWidth - 1; i <= maxWidth + 1; i += 1)
            {
                if (blocks[i, minDepth - 1])
                {
                    block b = blocks[i, minDepth - 1].GetComponent<block>();
                    code += $"block({i},{(int)(b.transform.position.y * 8)},{minDepth - 1},{b.BlockType}).\n";
                }
                if (blocks[i, maxDepth + 1])
                {
                    block b = blocks[i, maxDepth + 1].GetComponent<block>();
                    code += $"block({i},{(int)(b.transform.position.y * 8)},{maxDepth + 1},{b.BlockType}).\n";
                }


            }
            for (int j = minDepth - 1; j <= maxDepth + 1; j += 1)
            {
                if (blocks[minWidth - 1, j])
                {
                    block b = blocks[minWidth - 1, j].GetComponent<block>();
                    code += $"block({minWidth - 1},{(int)(b.transform.position.y * 8)},{j},{b.BlockType}).\n";
                }
                if (blocks[maxWidth + 1, j])
                {
                    block b = blocks[maxWidth + 1, j].GetComponent<block>();
                    code += $"block({maxWidth + 1},{(int)(b.transform.position.y * 8)},{j},{b.BlockType}).\n";
                }
            }

            print(code);
            return code;
        }

        public bool solved = true;

        int currentID;
        // Update is called once per frame
        void Update()
        {
            if (!solved && Solver.SolverStatus == ClingoSolver.Status.SATISFIABLE)
            {
                foreach (List<string> block in Solver.answerSet["block"])
                {

                    float x = int.Parse(block[0]);
                    float y = int.Parse(block[1]);
                    float z = int.Parse(block[2]);
                    string type = block[3];
                    if (type == "grass") BlockPrefab = GrassPrefab;
                    else if (type == "water") BlockPrefab = WaterPrefab;
                    else if (type == "sand") BlockPrefab = SandPrefab;

                    GameObject blockObj = Instantiate(BlockPrefab);
                    float xScale = blockObj.transform.localScale.x;
                    float zScale = blockObj.transform.localScale.z;
                    blockObj.transform.position = new Vector3(x * 2, y / 8, z * 2);
                    //removed a block if it has already been place in that location
                    if (blocks[(int)x, (int)z])
                        Destroy(blocks[(int)x, (int)z]);
                    blocks[(int)x, (int)z] = blockObj;
                }
                solvedAreas.Add(currentID);
                solved = true;
            }
            else if (!solved && Solver.SolverStatus == ClingoSolver.Status.UNSATISFIABLE)
            {
                int killArea = GetRandomNeighbor(currentID);
                print($"UNSATIFIABLE: removing area {killArea} pos {GetCoordinate(killArea, worldWidth)}");
                RemoveArea(killArea);
                buildQueue.Insert(0, killArea);
                buildQueue.Insert(0, currentID);
                solved = true;
            }
            else if (solved && buildQueue.Count > 0)
            {
                currentID = buildQueue[0];
                print($"Building area {currentID} pos {GetCoordinate(currentID, worldWidth)}");
                buildQueue.RemoveAt(0);
                Vector2 coordinates = GetCoordinate(currentID, worldWidth);
                int minWidth = ((int)coordinates.x - 1) * width + 1;
                int minDepth = ((int)coordinates.y - 1) * depth + 1;
                int maxWidth = ((int)coordinates.x - 1) * width + width;
                int maxDepth = ((int)coordinates.y - 1) * depth + depth;
                print($"min_width={minWidth} max_width={maxWidth} min_depth={minDepth} max_depth={maxDepth}");
                BuildArea(maxWidth, minWidth, height, 1, maxDepth, minDepth);
                solved = false;
            }
        }
        private int GetRandomNeighbor(int id)
        {
            Vector2 pos = GetCoordinate(id, worldWidth);
            List<int> neighbors = new List<int>();
            if (pos.x < worldWidth && solvedAreas.Contains(GetID(new Vector2(pos.x + 1, pos.y), worldWidth))) neighbors.Add(GetID(new Vector2(pos.x + 1, pos.y), worldWidth));
            if (pos.x > 1 && solvedAreas.Contains(GetID(new Vector2(pos.x - 1, pos.y), worldWidth))) neighbors.Add(GetID(new Vector2(pos.x - 1, pos.y), worldWidth));
            if (pos.y < worldDepth && solvedAreas.Contains(GetID(new Vector2(pos.x, pos.y + 1), worldWidth))) neighbors.Add(GetID(new Vector2(pos.x, pos.y + 1), worldWidth));
            if (pos.y > 1 && solvedAreas.Contains(GetID(new Vector2(pos.x, pos.y - 1), worldWidth))) neighbors.Add(GetID(new Vector2(pos.x, pos.y - 1), worldWidth));
            int index = Random.Range(0, neighbors.Count);
            return neighbors[index];
        }

        private Vector2 GetCoordinate(int id, int width)
        {
            int y = (id - 1) / width;
            int x = id - 1 - y * width;
            return new Vector2(x + 1, y + 1);
        }
        private int GetID(Vector2 coordinate, int width)
        {
            return (int)(coordinate.x - 1) + (int)(coordinate.y - 1) * width + 1;
        }

        public void ClingoSolve(string code, string parameter)
        {
            string path = ClingoUtil.CreateFile(code);
            Solver.Solve(path, parameter);
        }
    }
}