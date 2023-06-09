using Godot;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;

public class Game : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // [Export]
    // public NodePath _downConPath;

    [Export]
    public PackedScene _cupScene, _ballScene, _fireScene;

    private Node MobileAds;

    // public Ball _Ball;

    // public Control _downCon;

    // [Export]
    // public NodePath _Label1Path, _Label2Path;

    [Export]
    public NodePath _labelScorePath;


    // Label _Label1, _Label2;
    Label _labelScore;


    Random _random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // _Ball = GetNode<Ball>(_ballPath);
        MobileAds = (Node)GetNode("/root/MobileAds");
        MobileAds.Call("request_user_consent");
        MobileAds.Connect("initialization_complete", this, nameof(_on_MobileAds_initialization_complete));

        // _downCon = GetNode<Control>(_downConPath);

        // _Label1 = GetNode<Label>(_Label1Path);
        // _Label2 = GetNode<Label>(_Label2Path);

        _labelScore = GetNode<Label>(_labelScorePath);
        _labelScore.Text = "Score: " + Manager.Instance._score.ToString();

        Manager.Instance._game = this;
    }


    public void on_cup_disappear(Vector2 position)
    {
        GD.Print("on_cup_disappear", position);

        var firework = (Node2D)_fireScene.Instance();
        AddChild(firework);
        firework.GlobalPosition = position;

    }

    private void _on_MobileAds_initialization_complete(int status, String _adapter_name)
    {
        MobileAds.Call("load_banner");
    }

    // process
    public override void _Process(float delta)
    {
#if GODOT_ANDROID
        // var v = Input.GetGravity();
        // v.z = 0;
        // v.y = -v.y;

        // v.Normalized();

        // this._Label1.Text = "Gravity: " + v.ToString();
        // this._Label2.Text = "Gyroscope: " + Input.GetGyroscope().ToString();

        // Physics2DServer.AreaSetParam(GetViewport().FindWorld2d().Space, Physics2DServer.AreaParameter.GravityVector, v);
#endif

        // var children = _downCon.GetChildren();
        // int count = 0;
        // foreach (var item in children)
        // {
        //     if (item is Cup)
        //     {
        //         count++;
        //     }
        // }
        // if (count < 3)
        // {
        //     var cup = (Cup)_cupScene.Instance();
        //     cup.Position = new Vector2(0, -_random.Next(30, 300));
        //     _downCon.AddChild(cup);
        // }

        if (!_ticking)
        {
            return;
        }

        _timer += delta;

        _labelScore.Text = "Score: " + _timer.ToString("N2");
    }

    float _timer = 0;
    bool _ticking = false;

    public void go()
    {
        _timer = 0;
        _ticking = true;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            switch ((ButtonList)mouseEvent.ButtonIndex)
            {
                case ButtonList.Left:

                    if (!Manager.Instance._ball._moving)
                    {
                        Manager.Instance._ball.go(mouseEvent.Position);
                        Manager.Instance._game.go();
                    }

                    // GD.Print("Left button was clicked at ", mouseEvent.Position);

                    // var space = GetViewport().Size;

                    // if (mouseEvent.Position.y > space.y / 2)
                    // {
                    //     GD.Print("mouseEvent.Position.y > space.y / 2");
                    //     return;
                    // }

                    // instantiate a new ball
                    // var ball = _ballScene.Instance<Ball>();
                    // AddChild(ball);

                    // ball.Position = new Vector2(mouseEvent.Position.x, 100);

                    // ball._rigidBody2D.Mode = RigidBody2D.ModeEnum.Rigid;
                    // ball._rigidBody2D.CollisionLayer = 2;
                    // ball._rigidBody2D.CollisionMask = 2;

                    // ball.delayQueueFree();

                    // var fire = (Node2D)_fireScene.Instance();
                    // AddChild(fire);
                    // fire.Position = mouseEvent.Position;

                    // add force to drop it to left
                    // ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-100, 0));

                    // generate a random number between 1 and 100
                    // var random = new Random();
                    // var randomNumber = _random.Next(10, 200);

                    // GD.Print("randomNumber: ", randomNumber);

                    // ball._rigidBody2D.ApplyCentralImpulse(new Vector2(-randomNumber, 0));

                    // ball._rigidBody2D.AddForce(Vector2.Zero, new Vector2(-100, 0));

                    break;

            }
        }
    }

}
