using Godot;
using System;

public partial class player : Area2D
{

	[Signal]
	public delegate void CoinCollectedEventHandler(Coin coin);

	[Signal]
	public delegate void MissleHitEventHandler();

	public int Speed { get; set; } = 350; 
	public Vector2 ScreenSize; 

	public void PlayerDidCollide(Node2D body)
	{
		if (body is Coin) 
		{
			EmitSignal(SignalName.CoinCollected, body);
		} 
		else 
		{
			EmitSignal(SignalName.MissleHit);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("up"))
		{
			velocity.Y -= 1;
		}

		if (Input.IsActionPressed("down"))
		{
			velocity.Y += 1;
		}

		if (velocity.Length() > 0) 
		{
			velocity = velocity.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
}
