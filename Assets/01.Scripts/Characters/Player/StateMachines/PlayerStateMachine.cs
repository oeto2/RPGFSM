using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; } 

    //States
    public PlayerIdleState idleState { get; }

    public Vector2 MovementInput { get; set;}
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovemnetSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        idleState = new PlayerIdleState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundeData.BaseSpeed;
        RotationDamping = player.Data.GroundeData.BaseRotationDamping;
    }
}
