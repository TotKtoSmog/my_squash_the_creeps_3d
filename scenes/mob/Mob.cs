using Godot;

public partial class Mob : CharacterBody3D
{
	[Export]
	internal int MinSpeed { get; set; } = 10;
	[Export]
	internal int MaxSpeed { get; set; } = 18;

	[Signal]
	public delegate void SquashedEventHandler();

	public void Squash()
	{
		EmitSignal(SignalName.Squashed);
		QueueFree();
	}

	public override void _PhysicsProcess(double delta) => MoveAndSlide();

	public void Initialize(Vector3 startPosition, Vector3 playerPosition) 
	{
		int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);

		LookAtFromPosition(startPosition, playerPosition, Vector3.Up);

		RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));
	
		Velocity = Vector3.Forward * randomSpeed;
		Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
	}
	private void OnVisibilityNotifierScreenExited() => QueueFree();
}
