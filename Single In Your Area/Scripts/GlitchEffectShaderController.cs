using Godot;
using System;

public class GlitchEffectShaderController : Node2D
{
     // This script will set the split intensity of the glitch shader based on the 
	// anxiety stat (higher anxiety = greater glitchiness)
	// REQUIREMENTS: The ShaderMaterial assigned to this object must have the 
	// GlitchEffect shader
	
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
        mat.SetShaderParam("glitch_intensity", (float) statManager.GetStat("anxiety") / 100);
    }
}
