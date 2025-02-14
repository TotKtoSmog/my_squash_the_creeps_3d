using Godot;

public partial class Player : CharacterBody3D
{
	[Export]
	internal int Speed {get; set;} = 14;
	[Export]
	internal int FallAcceleration {get; set;} = 75;
	[Export]
	internal int JumpImpulse {get; set; } = 20;
	[Export]
	internal int BounceImpulse { get; set; } = 16;

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
		
		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
			_targetVelocity.Y = JumpImpulse;

		for (int index = 0; index < GetSlideCollisionCount(); index++)
		{
			
			KinematicCollision3D collision = GetSlideCollision(index);
			
			if (collision.GetCollider() is Mob mob)
			{
				if (Vector3.Up.Dot(collision.GetNormal()) > 0.1f)
				{
					mob.Squash();
					_targetVelocity.Y = BounceImpulse;
					break;
				}
			}
		}

		Velocity = _targetVelocity;
		MoveAndSlide();

	}
}
