using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	internal int Speed {get; set;} = 14;
	[Export]
	internal int FallAcceleration {get; set;} = 75;
	private Vector3 _targetVelocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 movmentInput = Input.GetVector("move_left","move_right","move_forward","move_back");

		Vector3 direction = new Vector3(movmentInput.X, 0, movmentInput.Y);
		
		if(direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
		}
		_targetVelocity =  new Vector3(direction.X, 0, direction.Z) * Speed;

		if(!IsOnFloor()) _targetVelocity.Y -= FallAcceleration * (float)delta;

		Velocity = _targetVelocity;

		MoveAndSlide();

	}
}
