shader_type canvas_item;

float rand(float co){
   return fract(sin(co * 78.233) * 43758.5453);
}

void fragment() {
	
	vec4 col = texture(TEXTURE, UV + rand(TIME));
	
	// Occasional purple pixels 
	float t = rand(UV.y + UV.x + TIME);
	if (t < 0.2) {
		col.r = 1.0;
		col.b = 1.0;
		col.g = t;
	}
	
    COLOR = col;
}