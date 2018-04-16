shader_type canvas_item;
uniform float glitch_intensity;
uniform float stripe_amount = 5.0;
uniform float stripe_fill = 0.7;
uniform float wave_frequency = 50.0;
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
		//stripe_intensity = new_glitch_intensity * 0.5;
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
 
    vec2 displUV = (displacement.xy * stripesRight) - (displacement.xy * stripesLeft);
    displUV += (displacement.zw * wavyDispl.r) - (displacement.zw * wavyDispl.g);

	// CHROMATIC ABERRATION 
	
    // Sample R G and B of adjacent pixels
    vec4 rightRed = texture(TEXTURE, UV + displUV + (0.03 * new_glitch_intensity));
    vec4 leftGreen = texture(TEXTURE, UV + displUV);
    vec4 diagBlue = texture(TEXTURE, UV + displUV + (0.06 * new_glitch_intensity));

    col.r = rightRed.r;
    col.g = leftGreen.g;
    col.b = diagBlue.b;

    // Assign the color to the output
    COLOR = col;
}