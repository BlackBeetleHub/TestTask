using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{

    public const int CELL = 0;
    public const int WALL = 1;
    public const int GROUND = 2;
    
    public const float SIZE_BLOCK = 0.5f;
    public const int MAZE_MULTIPLICITY = 5;

    public static int[,] maze;
    public static NavigateMaze navigateMaze;
    public static float timeSpend { private set; get; }

    public int height = 40;
    public int weigth = 40;
    public float coolDownCoin = 2.0f;

    public GameObject block;
    public GameObject ground;
    public GameObject coint;


    public virtual void SetWall(float x, float y)
    {
        Instantiate(block, new Vector3(x, y, block.transform.position.z), Quaternion.identity);
    }

    public virtual void SetGround(float x, float y)
    {
        Instantiate(ground, new Vector3(x, y, block.transform.position.z), Quaternion.identity);
    }

    public virtual void Initial()
    {
        Block[,] tmpMaze = new Block[height, weigth];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < weigth; j++)
            {
                if (maze[i, j] == GROUND)
                {
                    SetGround(SIZE_BLOCK * j, SIZE_BLOCK * i);
                    tmpMaze[i, j] = new Ground(j, i, new Vector2(SIZE_BLOCK * j, SIZE_BLOCK * i));
                }
                else
                {
                    SetWall(SIZE_BLOCK * j, SIZE_BLOCK * i);
                    tmpMaze[i, j] = new Wall(j, i, new Vector2(SIZE_BLOCK * j, SIZE_BLOCK * i));
                }
            }
        }
        navigateMaze = new NavigateMaze(tmpMaze, height, weigth);
    }

    void Start()
    {
        Maze res = new Maze(new DeepMazeBuilder(height, weigth, MAZE_MULTIPLICITY));
        maze = res.GetArrayMaze();
        Initial();
        timeSpend = 0;
    }

    public void Update()
    {
        timeSpend += Time.deltaTime;
    }
}
