using System.Security.Cryptography.X509Certificates;
using Godot;


public partial class Movement : CharacterBody2D
{
	public static float Speed = 500.0f;
	public static float JumpVelocity = -400.0f;
	public const float BaseAcc = 4.0f;
	public static float Acc = BaseAcc;
	public static float DesAcc = 3.0f;
	public static float Mass = 1.0f;

	private Camera2D camera;
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
		camera = GetNode<Camera2D>("Camera2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		//Declare Functions
		Get_Input(delta);
		MoveAndSlide();
		GD.Print("X: ", Position.X);
		GD.Print("Y: ", Position.Y);
		GD.Print("Velocidade: ", Velocity);

	}
	public void Get_Input(double delta)
	{
		//Gravity and Movement
		Godot.Vector2 velocity = Velocity;
		//Declare Rotation Value
		float rotationSpeed = 0.04f;



		//Jumping
		if (!IsOnFloor())
		{
			Acc = DesAcc;
			velocity.Y += gravity * (float)delta * Mass;
		}
		else
		{
			Acc = BaseAcc;
		}
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = Mathf.Min(velocity.Y + 0, JumpVelocity);
			AudioStream.Play();
		}

		//Idle Animation
		animationPlayer.Play("idle");
		//Rotation
		RotationDegrees += rotationSpeed * velocity.X;

		//Rolling
		if (Input.IsActionPressed("right"))
		{
			animationPlayer.Stop();
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Min(velocity.X + Acc, Speed);


		}
		else if (Input.IsActionPressed("left"))
		{
			animationPlayer.Stop();
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Max(velocity.X - Acc, -Speed);
		}
		//Stopping
		else
		{
			float Friction = 0.01f;
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction);
		}
		Velocity = velocity;
	}
}

