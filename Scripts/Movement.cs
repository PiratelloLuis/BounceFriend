using System.Collections;
using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Godot;


public partial class Movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float Acc = 5.0f;	
	public const int health = 3;
	public const float Jf = 10.0f;


	private AnimationPlayer animationPlayer;
	private Sprite2D playerSprite;
	private AudioStreamPlayer2D AudioStream;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	

    public override void _Ready()
    {
		//Declare Nodes
		AudioStream = GetNode<AudioStreamPlayer2D>("Jump_sound");
		playerSprite = GetNode<Sprite2D>("ball");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
	{
		//Declare Functions
		Get_Input(delta);
		MoveAndSlide();
		Die();
	}
	public void Get_Input(double delta){
		//Audios

		//Gravity and Movement
		Godot.Vector2 velocity = Velocity;
		//Declare Rotation Values
		float foward_rotationSpeed = 0.04f;
		float back_rotationSpeed = -0.04f;

		
		//Jumping
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}
		if (Input.IsActionJustPressed("jump") && IsOnFloor()){
			velocity.Y = Mathf.Min(velocity.Y + Jf, JumpVelocity);
			AudioStream.Play();
		}


		//Rolling
		if (Input.IsActionPressed("right"))
		{
			animationPlayer.Stop();
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Min(velocity.X + Acc, Speed);
			if (velocity.X > 0){
			RotationDegrees += foward_rotationSpeed * Speed;
		}
		}
		else if (Input.IsActionPressed("left")){
			animationPlayer.Stop();
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Max(velocity.X - Acc, - Speed);
			if(Velocity.X < 0){
			RotationDegrees += back_rotationSpeed * Speed;
		}
		}
		//Stopping
		else
		{
			float Friction = 0.05f;
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction);
			animationPlayer.Play("idle");
		}
		Velocity = velocity;
	}

	public void Die(){
		if (health < 1){
			GetTree().ReloadCurrentScene();
		}
	}

	public void Inertia(){
		
	}

	}

