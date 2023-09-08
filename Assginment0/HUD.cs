using Godot;
using System;

public partial class HUD : CanvasLayer
{

	[Signal]
	public delegate void StartGameEventHandler();

	public void StartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		GetNode<Label>("Message").Show();
		GetNode<Timer>("StartTimer").Start();

	}

	public void TimerTimeout() 
	{
		GetNode<Label>("Message").Hide();
		EmitSignal(SignalName.StartGame);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("StartButton").Show();
		GetNode<Label>("Message").Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
