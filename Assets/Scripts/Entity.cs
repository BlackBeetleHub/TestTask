using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    protected bool isFlip = false;
    protected Block current;
    protected Block direction;

    public void Flip(bool value)
    {
        if (value == isFlip)
        {
            return;
        }
        isFlip = value;
        Vector3 vec = transform.localScale;
        vec.x *= -1;
        transform.localScale = vec;
    }

    public virtual void WalkDown()
    {
        Block tmp = Labyrinth.navigateMaze.GetBlock(current.i, current.j - 1);
        if (tmp != null)
        {
            direction = tmp;
        }
    }

    public virtual void WalkLeft()
    {
        Block tmp = Labyrinth.navigateMaze.GetBlock(current.i + 1, current.j);
        if (tmp != null)
        {
            direction = tmp;
        }
    }

    public virtual void WalkRight()
    {
        Block tmp = Labyrinth.navigateMaze.GetBlock(current.i - 1, current.j);
        if (tmp != null)
        {
            direction = tmp;
        }
    }

    public virtual void WalkUp()
    {
        Block tmp = Labyrinth.navigateMaze.GetBlock(current.i, current.j + 1);
        if (tmp != null)
        {
            direction = tmp;
        }
    }

    public virtual void Start()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        int i = (int)(x / 0.5f);
        int j = (int)(y / 0.5f);
        current = new Block(i, j, (transform.position));
        direction = Labyrinth.navigateMaze.GetRandomDirection(current);
    }

    public virtual void ExecuteAILogic()
    {

    }

    public virtual void Update()
    {
        if (transform.position.x == direction.GetPosition().x && transform.position.y == direction.GetPosition().y)
        {
            current = (Block)direction.Clone();
            ExecuteAILogic();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, direction.GetPosition(), 2 * Time.deltaTime);
        }
    }

    public abstract void Attack();

    public abstract void GetHit(Entity entity);

}