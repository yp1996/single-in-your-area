using Godot;
using System;

public class Player : KinematicBody2D
{
	
	bool isConscious = true;

    const float walkSpeed = 100f;
	public bool isMoving = false;
	int currentFrame = 0;
	const int maxFrames = 4;
	enum Direction: int {Left = 12, Right = 8, Up = 0, Down = 4}; // value is number of frame

	int stepsTaken = 0;
	int anxietyModifier = 1; // how fast anxiety increases
	Direction currentDirection = Direction.Up;
	Sprite sprite;
    NavPoint navPt;
	Timer passOutTimer;
	
	AnimationPlayer passOutAnimation;
	
	Vector2 offset = new Vector2(0,30); // offset in order not to block the character by the nav arrow
	Vector2 defaultPosition = new Vector2(470,270);
	StatManager statManager;

    public override void _Ready()
    {
        GD.Print("HELLO THERE");
        // Called every time the node is added to the scene.
        // Initialization here
        navPt = (NavPoint)GetNode("../navpoint_container");
        sprite = (Sprite)GetNode("Sprite");
		statManager = (StatManager) GetNode("/root/StatManager");
        passOutAnimation = (AnimationPlayer) GetNode("../../CanvasLayer/PassOut");
   		passOutTimer = (Timer) GetNode("PassOutTimer");
 }

    public override void _Process(float delta)
    {
		if (isConscious) {
		
			if (IsPassedOut()) {
				PassOut();
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
			stepsTaken = (int) (movePosition.Normalized()*walkSpeed).Length();
			
			if (GetSlideCount() > 0) {
				CheckCollectibles();
			}
		}
	}
	
	private bool IsPassedOut() {
		return (statManager.GetStat("health") == 0 || statManager.GetStat("anxiety") == 100);
	}
	
	private void OnTimerTimeout() {
		
		if (isConscious) {
			if (isMoving) {
				currentFrame = (currentFrame + 1) % maxFrames;
			}
			
			if (stepsTaken > 0) {
				statManager.IncrementStat("anxiety", anxietyModifier);
			} else {
				statManager.IncrementStat("anxiety", -1);
			}	
		}
	}
	
	private void CheckCollectibles() {
		
		KinematicCollision2D collision = GetSlideCollision(0);
		Node2D body = (Node2D) collision.Collider;
		if (body.GetParent().GetParent().GetName().Contains("Collectible")) {
			
			GD.Print(body.GetParent().GetParent().GetParent().GetName());
			((Collectible) body.GetParent().GetParent()).Consume();
			((Node2D) body.GetParent()).QueueFree();
		} else if (body.GetName().Contains("SceneSwitcher")) {
			((SceneSwitcher) body).SwitchScene();
		}
	}
	
	private void PassOut() {
		isConscious = false;
		stepsTaken = 0;
		anxietyModifier += 1;
		statManager.SetStat("anxiety", 0);
		passOutTimer.Start();
		passOutAnimation.Play("fade");
	}
	
	private void OnPassOutTimeout(){
		navPt.clickPosition = defaultPosition;
		SetPosition(defaultPosition);
		isConscious = true;
		statManager.SetStat("health", 100);

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

