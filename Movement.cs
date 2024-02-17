using Godot;


public partial class Movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float Acc = 5.0f;	

	public const float Jf = 10.0f;


	private AnimationPlayer animationPlayer;
	private Sprite2D playerSprite;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
		//Declare Nodes
		playerSprite = GetNode<Sprite2D>("ball");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
	{
		//Declare Functions
		Get_Input(delta);
		MoveAndSlide();
	}
	public void Get_Input(double delta){
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
		}


		//Rolling
		if (Input.IsActionPressed("right"))
		{
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Min(velocity.X + Acc, Speed);
			if (velocity.X > 0){
			RotationDegrees += foward_rotationSpeed * Speed;
		}
		}
		else if (Input.IsActionPressed("left")){
			animationPlayer.Play("rolling");
			velocity.X = Mathf.Max(velocity.X - Acc, - Speed);
			if(Velocity.X < 0){
			RotationDegrees += back_rotationSpeed * Speed;
		}
		}
		//Stopping
		else
		{
			float Inertia = 0.09f;
			velocity.X = Mathf.Lerp(velocity.X, 0, Inertia);
			animationPlayer.Play("idle");
		}
		Velocity = velocity;
	}

	public void Die(){
		GetTree().ReloadCurrentScene();
	}
	}

