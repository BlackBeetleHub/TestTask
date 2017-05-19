using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class NavigateMaze
{

    public int height { private set; get; }
    public int width { private set; get; }
    public Block[,] maze { private set; get; }

    private System.Random random;

    public NavigateMaze(Block[,] mazeList, int height, int width)
    {
        random = new System.Random();
        this.height = height;
        this.width = width;
        maze = mazeList;
    }

    public Block GetBlock(int x, int y)
    {
        if (maze[y, x].ToString() != "Wall")
        {
            return maze[y, x];
        }
        return null;
    }

    public List<Block> GetNeighbors(Block position)
    {
        List<Block> blocks = new List<Block>();
        try
        {
            if (maze[position.j + 1, position.i].ToString() != "Wall")
            {
                blocks.Add(maze[position.j + 1, position.i]);
            }
            if (maze[position.j - 1, position.i].ToString() != "Wall")
            {
                blocks.Add(maze[position.j - 1, position.i]);
            }
            if (maze[position.j, position.i + 1].ToString() != "Wall")
            {
                blocks.Add(maze[position.j, position.i + 1]);
            }
            if (maze[position.j, position.i - 1].ToString() != "Wall")
            {
                blocks.Add(maze[position.j, position.i - 1]);
            }
        }catch(Exception exception)
        {
            Debug.Log(exception.ToString());
        }
        return blocks;
    }

    public Block GetRandomDirection(Block position)
    {
        List<Block> blocks = GetNeighbors(position);
        if (blocks.Count != 0)
        {
            return blocks[random.Next(0, blocks.Count)];
        }
        return null;
    }

}
