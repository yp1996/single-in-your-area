using Godot;
using System;

public class Player : KinematicBody2D
{

    const float walkSpeed = 100f;
	public bool isMoving = false;
	int currentFrame = 0;
	const int maxFrames = 4;
	enum Direction: int {Left = 4, Right = 8, Up = 0, Down = 12}; // value is number of frame 
	
	const int timeBetweenUpdates = 100;
	int timeLastUpdate = 0;
    
	Direction currentDirection = Direction.Up;
	Sprite sprite;
    NavPoint navPt;
	Vector2 offset = new Vector2(0,30); // offset in order not to block the character by the nav arrow

    public override void _Ready()
    {
        GD.Print("HELLO THERE");
        // Called every time the node is added to the scene.
        // Initialization here
        navPt = (NavPoint)GetNode("../navpoint_container");
        sprite = (Sprite)GetNode("Sprite");
        
    }

    public override void _Process(float delta)
    {
	
		if ((OS.GetTicksMsec() - timeLastUpdate > timeBetweenUpdates) && isMoving) {
			currentFrame = (currentFrame + 1) % maxFrames;
			timeLastUpdate = OS.GetTicksMsec();
		}
		
		sprite.Frame = (int) currentDirection + currentFrame;
		
		Vector2 targetPos = navPt.clickPosition + offset;
		Vector2 movePosition = targetPos - GetPosition();
		
		if (movePosition.Length() <= 1) { // Comparing to 1 vs 0 to avoid weird jitter
			movePosition = new Vector2(0,0);
			isMoving = false;
			currentFrame = 0;
		} else {
			isMoving = true;
			UpdateDirection(movePosition);
		}
		
        MoveAndSlide((movePosition.Normalized()*walkSpeed));
		
    }
	
	private void UpdateDirection(Vector2 movePosition) {
		if (Mathf.Abs(movePosition.x) > Mathf.Abs(movePosition.y)) {
				if (movePosition.x > 0) {
					currentDirection = Direction.Right;
				} else {
					currentDirection = Direction.Left;
				}
		} else {
			if (movePosition.y > 0) {
				currentDirection = Direction.Up;
			} else {
				currentDirection = Direction.Down;
			}
		}
		
	}
}
