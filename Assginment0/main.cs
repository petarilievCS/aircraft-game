using Godot;
using System;

public partial class main : Node
{
	private const int X_START = 40;
	private const int X_END = 440;
	private const int Y_POSITION = 0;

	private const int COIN_SPEED = 350;
	private const int MISSLE_SPEED = 5;
	
	private PackedScene _missleScene;
	private PackedScene _coinScene; 
	private int _score; 

	public void SpawnCoin() 
	{
		var coin = _coinScene.Instantiate<Coin>();
		coin.Position = GetRandomPosition();
		coin.LinearVelocity = new Vector2(0, COIN_SPEED);
		AddChild(coin);
	}

	public void SpawnMissle() 
	{
		var missle = _missleScene.Instantiate<missle>();
		missle.Position = GetRandomPosition();
		missle.LinearVelocity = new Vector2(0, MISSLE_SPEED);
		AddChild(missle);
	}

	public void CoinCollected(Coin coin)
	{
		_score += 1;
		GetNode<Label>("HUD/Score").Text = _score.ToString();
		coin.QueueFree();
	}

	public void MissleHit()
	{
		GameOver();
	}

	public Vector2 GetRandomPosition()
	{
		GD.Randomize();
		return new Vector2(GD.RandRange(X_START, X_END), Y_POSITION);
	}

	public void NewGame() 
	{
		_score = 0;
		GetNode<Label>("HUD/Score").Text = _score.ToString();
		var player = GetNode<Area2D>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Show();
		player.Position = startPosition.Position;

		GetNode<Timer>("CoinTimer").Start();
		GetNode<Timer>("EnemyTimer").Start();
	}

	public void GameOver()
	{
		GetTree().CallGroup("coins", "queue_free");
		GetTree().CallGroup("missles", "queue_free");
		GetNode<Timer>("CoinTimer").Stop();
		GetNode<Timer>("EnemyTimer").Stop();
		GetNode<Area2D>("Player").Hide();
		
		Button startButton = GetNode<Button>("HUD/StartButton");
		startButton.Text = "Restart";
		startButton.Show();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coinScene = GD.Load<PackedScene>("res://coin.tscn");
		_missleScene = GD.Load<PackedScene>("res://missle.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
