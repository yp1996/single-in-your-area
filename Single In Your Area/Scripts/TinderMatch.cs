using Godot;
using System;
using System.Collections.Generic;

public class TinderMatch : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    private Sprite sprite;
	private Sprite nextSprite;
	private RichTextLabel label;
	private Vector2 lastMousePosition; 
	private Vector2 screenSize;
	private Vector2 screenCentre;
	private float slideSpeed = 10f;
	
	private float cooldownTime = 0.5f;
	private float maxCooldown = 0.5f;
	
	public string scene = "res://Scenes/Levels/Bedroom.tscn";
	private bool isSwitching = false;
	
	public struct TinderProfile {
		public Texture picture;
		public string bio;
		
		public TinderProfile(string path, string bioText) {
			picture = (Texture) GD.Load("res://UI/findr/" + path + ".png");
			bio = bioText;
		}
	}
	
	private List<TinderProfile> tinderProfiles;
	private TinderProfile currentProfile;
	private TinderProfile nextProfile;
	private int currentIndex = 0;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        sprite = (Sprite)GetNode("Pic");
		nextSprite = (Sprite)GetNode("NextPic");
		label = (RichTextLabel)GetNode("Bio");
		screenSize = GetViewportRect().Size;		
		screenCentre = screenSize / 2f;
		lastMousePosition = screenCentre;
		
		tinderProfiles = new List<TinderProfile>();
		tinderProfiles.Add(new TinderProfile("dead", "Not like the other girls. I'm dead"));
		tinderProfiles.Add(new TinderProfile("onfire", "In She’ol but movin’ on up to the world to come"));
		tinderProfiles.Add(new TinderProfile("fishing", "I beach long walks on the enjoy"));
		tinderProfiles.Add(new TinderProfile("skeleycat", "Looking for the Dennett to my Chomsky. Enjoy well-read, thoughtful people and intellectual discussions. Sapiosexual."));
		tinderProfiles.Add(new TinderProfile("spookyskeleton", "I’ve been dead since Wednesday lmao"));
		tinderProfiles.Add(new TinderProfile("slime", "Not technically 'alive' (wednesday amirite?) but looking to meet new 'people'"));
		tinderProfiles.Add(new TinderProfile("ina", "“Somehow survived” Wednesday, so ofc I’m on Findr"));
    	
		currentProfile = tinderProfiles[currentIndex];
		nextProfile = tinderProfiles[currentIndex + 1];
		nextSprite.SetTexture(nextProfile.picture);
		
	}
	
	public override void _Process(float delta) {
		
		if (sprite.IsVisible() && (!isSwitching)) {
		
		if (cooldownTime <= 0f) { // Block swiping so that you don't swipe through all profiles at once
		
			Vector2 currentPosition = sprite.GetPosition();
			
			if (Input.IsActionPressed("left_click")) {
				
				Vector2 clickPosition = GetGlobalMousePosition();
	            float deltaClick = clickPosition.x - lastMousePosition.x;
				sprite.SetPosition(new Vector2(clickPosition.x, sprite.GetPosition().y));
				lastMousePosition = clickPosition;
			}   
			
			else {
				float distanceFromCentre = Mathf.Min(Mathf.Abs(currentPosition.x - screenCentre.x), slideSpeed);
				currentPosition.x += distanceFromCentre * Mathf.Sign(screenCentre.x - currentPosition.x);
				sprite.SetPosition(currentPosition);
				nextSprite.SetPosition(new Vector2(screenSize.x - currentPosition.x, currentPosition.y));
			}
			
			if (hasSwipedLeft(currentPosition)) {
				currentIndex++;
				if (currentIndex < tinderProfiles.Count) {
					cooldownTime = maxCooldown;
					currentProfile = tinderProfiles[currentIndex];
					if (currentIndex < tinderProfiles.Count - 1) {
						nextProfile = tinderProfiles[currentIndex + 1];
						nextSprite.SetTexture(nextProfile.picture);
					}
					else {
						nextSprite.SetTexture(null);
					}
					sprite.SetTexture(currentProfile.picture);
					label.SetText(currentProfile.bio);
					currentPosition.x = screenCentre.x;
					sprite.SetPosition(currentPosition);
					nextSprite.SetPosition(currentPosition);
					
				}
			}
			
			if (hasSwipedRight(currentPosition)) {

				var global = (Global)GetNode("/root/Global");
                global.GotoScene(scene);
				isSwitching = true;
			}
		}
		
		else {
			cooldownTime -= delta;
		}
		}
		  
	}
	
	private bool hasSwipedLeft(Vector2 currentPosition) {
		// For the first n-1 profiles you can only swipe left 
		return (currentPosition.x <= 0f && currentIndex < tinderProfiles.Count - 1);
	}
	
	private bool hasSwipedRight(Vector2 currentPosition) {
		// For the first n-1 profiles you can only swipe left 
		return (currentPosition.x >= screenSize.x && currentIndex >= tinderProfiles.Count - 1) && (!isSwitching);
	}




}





