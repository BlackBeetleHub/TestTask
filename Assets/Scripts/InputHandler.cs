using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Command
{
    void execute(CharacterController gameActor);
}

class WalkCommand : Command
{
    public virtual void execute(CharacterController gameActor)
    {
        gameActor.Flip(InputHandler.isFlip);
        if (!InputHandler.isFlip)
        {
            gameActor.WalkLeft();
        }
        else
        {
            gameActor.WalkRight();
        }
    }

    public override string ToString()
    {
        return "WalkCommand";
    }
}

class WalkCommandUp : WalkCommand
{
    public override void execute(CharacterController gameActor)
    {
        gameActor.WalkUp();
    }
}

class WalkCommandDown : WalkCommand
{
    public override void execute(CharacterController gameActor)
    {
        gameActor.WalkDown();
    }
}

class StayCommand : Command
{
    public void execute(CharacterController gameActor)
    {
        gameActor.Stay();
    }
    public override string ToString()
    {
        return "StayCommand";
    }
}

public class InputHandler
{
    public static bool isFlip = false;

    private Command lastCommand = new WalkCommand();

    public Command inputHandler()
    {
        if (Input.GetKey(KeyCode.D))
        {
            isFlip = false;
            lastCommand = new WalkCommand();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            isFlip = true;
            lastCommand = new WalkCommand();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            lastCommand = new WalkCommandUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            lastCommand = new WalkCommandDown();
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            FileManager.WriteInfo("Escape");
            Application.LoadLevel("menu");
        }
        return lastCommand;
    }
}
