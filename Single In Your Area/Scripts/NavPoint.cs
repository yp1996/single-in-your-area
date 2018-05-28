using Godot;
using System;

public class NavPoint : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	public Vector2 clickPosition;
	bool goingUp = true;
	float stepSize = 0.25f;
	int cycleLength = 500;
	int timeLastUpdate = 0;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

    }

   public override void _Process(float delta)
   {
		MovePointer(); 
		
        if (Input.IsActionPressed("left_click")) {
			
			clickPosition = GetGlobalMousePosition();
            this.SetPosition(clickPosition);
		}       
   }
	
   public void MovePointer() {
	
	    if (OS.GetTicksMsec() - timeLastUpdate > cycleLength) {
			goingUp = !goingUp;
			timeLastUpdate = OS.GetTicksMsec();
		}
		if (goingUp) {
			this.SetPosition(this.GetPosition() + new Vector2(0f, stepSize));
		} else {
			this.SetPosition(this.GetPosition() - new Vector2(0f, stepSize));
		}
	}
}
