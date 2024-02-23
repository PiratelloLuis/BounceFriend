using System;
using Godot;



public partial class OldMovement : CharacterBody2D
{
	public float Acc = 1.0f;
	public float FinalSpeed = 1.0f;
	public float StaticSpeed = 0.0f;
	public float MaxSpeed = 300.0f;
	public float JumpVelocity = 500.0f;
	private AnimationPlayer animationPlayer;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public bool movementX1;
	public bool movementX2;
	
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public override void _Ready()
    {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        Godot.Vector2 velocity = Velocity;
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}

		//pulo
		if (Input.IsKeyPressed(Key.Space) && IsOnFloor()){
			velocity.Y = -JumpVelocity;
		}
		
		movementX1 = false;
		movementX2 = false;
		velocity.X = StaticSpeed;
		

		// movimento para os lados
		if (Input.IsKeyPressed(Key.Left)){
			movementX1 = true;
			velocity.X = -FinalSpeed;
		}
		if (Input.IsKeyPressed(Key.Right)){
			movementX2 = true;
			velocity.X = FinalSpeed;			
}
 

		Velocity = velocity;
		Acceleration();
		RotateSprite();
		MoveAndSlide();
		Debug();

		 if (velocity.X == 0)
        {
            animationPlayer.Play("idle");
        }
        else
        {
            animationPlayer.Play("rolling");
        }
    }

	public void RotateSprite(){
		if (movementX1){
			float rotationSpeed = -0.04f;
			RotationDegrees += rotationSpeed * FinalSpeed;
		}
		else if(!movementX1){
			RotationDegrees = RotationDegrees;

		}
		if (movementX2){
			float rotationSpeed = 0.04f;
			RotationDegrees += rotationSpeed * FinalSpeed ;
		}
		else if(!movementX2){
			RotationDegrees = RotationDegrees;

		}
	
	}
	public void Acceleration(){
		if(FinalSpeed<MaxSpeed && Velocity.X != StaticSpeed){
			FinalSpeed += Acc * 8;	
		}
		if(Velocity.X == StaticSpeed){
			FinalSpeed = Acc;
		}
		
	}
	public void Debug(){
		GD.Print(Acc);
		GD.Print(FinalSpeed);
	}
}
