using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CharacterController : Human
{

    public static Block position;
    public InputHandler Handle;

    public void Stay()
    {
        // GetComponent<Animator>().SetBool("Stay", true);
    }

    void FixedUpdate()
    {
        Command command = Handle.inputHandler();
        command.execute(this);
    }

    public override void Start()
    {
        base.Start();
        position = current;
        Handle = new InputHandler();
    }

    public override void Update()
    {
        base.Update();
        position = current;
    }
    
}
