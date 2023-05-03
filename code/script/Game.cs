using Godot;
using System;
using System.CodeDom.Compiler;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public NodePath _BallPath;


    public Ball _Ball;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _Ball = GetNode<Ball>(_BallPath);
    }


    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            switch ((ButtonList)mouseEvent.ButtonIndex)
            {
                case ButtonList.Left:
                    // GD.Print("Left button was clicked at ", mouseEvent.Position);

                    // instantiate a new ball
                    var ball = (Ball)_Ball.Duplicate();
                    AddChild(ball);
                    // ball.Position = mouseEvent.Position;

                    ball._rigidBody2D.Mode = RigidBody2D.ModeEnum.Rigid;
                    ball._rigidBody2D.CollisionLayer = 2;
                    ball._rigidBody2D.CollisionMask = 2;


                    // add force to drop it to left
                    // ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-100, 0));

                    // generate a random number between 1 and 100
                    var random = new Random();
                    var randomNumber = random.Next(1, 100);

                    ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-randomNumber, 0));

                    // ball._rigidBody2D.AddForce(Vector2.Zero, new Vector2(-100, 0));

                    break;

            }
        }
    }

}
