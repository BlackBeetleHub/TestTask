using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Block : ICloneable
{

    public int i { private set; get; }
    public int j { private set; get; }
    private Vector2 position;

    public Block(int i, int j, Vector2 position)
    {
        this.i = i;
        this.j = j;
        this.position = position;
    }

    public object Clone()
    {
        Block block = new Block(i, j, position);
        return block;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

}

public class Wall : Block
{

    public Wall(int i, int j, Vector2 position) : base(i, j, position)
    {

    }

    public override string ToString()
    {
        return "Wall";
    }

}

public class Ground : Block
{

    public Ground(int i, int j, Vector2 position) : base(i, j, position)
    {

    }

    public override string ToString()
    {
        return "Ground";
    }

}