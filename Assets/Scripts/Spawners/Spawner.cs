using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    
    public static int count;

    public float time;
    public new GameObject gameObject;

    protected float currentTime;

    private System.Random random;


    public virtual void Start()
    {
        count = 0;
        currentTime = time;
        random = new System.Random();
    }

    public virtual void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = time;
            Block tmp = null;
            int x;
            int y;
            do
            {
                x = random.Next(0, Labyrinth.navigateMaze.width);
                y = random.Next(0, Labyrinth.navigateMaze.height);
                tmp = Labyrinth.navigateMaze.GetBlock(x, y);
            } while (tmp == null);
            Instantiate(gameObject, tmp.GetPosition(), Quaternion.identity);
            Increase();
        }
    }

    public virtual void Increase()
    {
        count++;
    }

    public virtual void Decrease()
    {
        count--;
    }
    
}

public class ZombieSpawner : Spawner
{
    public static new int count;

    public override void Increase()
    {
        count++;
    }

    public override void Decrease()
    {
        count--;
    }
}

public class MummySpawner : Spawner
{
    public static new int count;

    public override void Increase()
    {
        count++;
    }

    public override void Decrease()
    {
        count--;
    }
}
