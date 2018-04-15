using Godot;
using System;

public class SaturationShaderController : Node2D
{
    // This script will set the saturation of the shader based on the 
	// anxiety stat (higher anxiety = lower saturation).
	// REQUIREMENTS: The ShaderMaterial assigned to this object must have the 
	// SaturationControl shader
	
	ShaderMaterial mat;
	StatManager statManager;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        statManager = (StatManager) GetNode("/root/StatManager");
		mat = (ShaderMaterial) GetMaterial();
    }

    public override void _Process(float delta)
    {
		// Update the saturation uniform 
        mat.SetShaderParam("saturation", 1f - (float) statManager.GetStat("anxiety") / 100);
    }
}
