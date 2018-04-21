shader_type canvas_item;
uniform float glitch_intensity;
uniform float stripe_amount = 2.0;
uniform float stripe_fill = 0.7;
uniform float wave_frequency = 30.0;
uniform sampler2D noise_texture;
uniform vec4 displacement = vec4(0.0, 0.0, 0.01, 0.1);


float rand(float co){
   return fract(sin(co * 78.233) * 43758.5453);
}

void fragment() {
	
	float new_glitch_intensity = (0.9 + rand(TIME)*0.1)*glitch_intensity;
	vec4 col = texture(TEXTURE, UV);
	
	// STRIPES 
	float stripe_intensity = clamp(log2(new_glitch_intensity*2.0), 0.0, 1.0);
	float wave_intensity = stripe_intensity;
	if (new_glitch_intensity <= 0.7) {
		wave_intensity = 0.0;
	} else if (new_glitch_intensity <= 0.5) {
		stripe_intensity = 0.0; // No stripes for small glitch effects 
		wave_intensity = 0.0;
	} 
	float stripesRight = floor(UV.y * stripe_amount * new_glitch_intensity);
    stripesRight = step(stripe_fill, rand(TIME));
 
    float stripesLeft = floor(UV.y * stripe_amount * new_glitch_intensity);
    stripesLeft = step(stripe_fill, rand(TIME));
	
	// WAVY DISPLACEMENT
	
	vec4 wavyDispl = mix(vec4(1.0,0.0,0.0,1.0), vec4(0.0,1.0,0.0,1.0), (sin(UV.y * wave_frequency * wave_intensity) + 1.0) / 2.0);
 
	if (new_glitch_intensity <= 0.1) {
		wavyDispl = vec4(0.0, 0.0, 0.0, 0.0);
	}
	
    vec2 displUV = (displacement.xy * stripesRight) - (displacement.xy * stripesLeft);
    displUV += (displacement.zw * wavyDispl.r) - (displacement.zw * wavyDispl.g);

	// CHROMATIC ABERRATION 
	
    // Sample R G and B of adjacent pixels
    vec4 rightRed = texture(TEXTURE, UV + displUV + (0.05 * new_glitch_intensity));
    vec4 leftGreen = texture(TEXTURE, UV + displUV);
    vec4 diagBlue = texture(TEXTURE, UV + displUV - (0.07 * new_glitch_intensity));

	float finalAlpha = col.a;

    if ((diagBlue.a > 0.0 && rightRed.a > 0.0 && leftGreen.a > 0.0)) { // If sampled textures exist, make visible
		finalAlpha = 1.0;
	}
	if (diagBlue.b == 0.0 && rightRed.r == 0.0 && leftGreen.g == 0.0) { // If the result is black, turn invisible
		finalAlpha = 0.0;
	}
	
    col.r = rightRed.r;
    col.g = leftGreen.g;
    col.b = diagBlue.b;

	float distortion = 1.0 - texture(noise_texture, UV + displUV).r*glitch_intensity*0.5;
	
    // Assign the color to the output
    COLOR = vec4(col.rgb * distortion, finalAlpha);
}