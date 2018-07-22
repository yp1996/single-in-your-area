shader_type canvas_item;

void fragment() {

	float sizeX = 2.0;
	float sizeY = 2.0;
    // Fetch the texture's pixel
    vec4 col = texture(TEXTURE, UV); 
	
	float newX = UV.x + TIME / 20.0;
	vec4 newCol = texture(TEXTURE, vec2(newX, UV.y));
	
	float baseG = 0.8;
	float baseR = 0.7;
	float baseB = 0.7;
	
	
	if (newCol.r != 1.0 && newCol.g != 1.0 && newCol.b != 1.0) {
		newCol.g = baseG;
		newCol.r = min(newCol.r, 0.3) + baseR + 0.2*abs(sin(TIME));
		newCol.b = min(newCol.b, 0.3) + baseB + 0.2*abs(cos(TIME));
	}
	
	
    COLOR = newCol;
}