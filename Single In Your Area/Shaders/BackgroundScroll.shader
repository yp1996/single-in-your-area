shader_type canvas_item;

uniform vec3 baseColor = vec3(0.7, 0.8, 0.7);
uniform vec3 colChange = vec3(0.2, 0.0, 0.2);
uniform vec4 foregroundColor = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {

    // Fetch the texture's pixel
    vec4 col = texture(TEXTURE, UV); 
	
	float newX = UV.x + TIME / 20.0;
	vec4 newCol = texture(TEXTURE, vec2(newX, UV.y));
	
	float baseG = baseColor.g;
	float baseR = baseColor.r;
	float baseB = baseColor.b;
	
	
	if (newCol.r != 1.0 && newCol.g != 1.0 && newCol.b != 1.0) {
		newCol.g = baseG;
		newCol.r = min(newCol.r, 0.3) + baseR + colChange.r*abs(sin(TIME));
		newCol.b = min(newCol.b, 0.3) + baseB + colChange.b*abs(cos(TIME));
	} 
	else {
		newCol = foregroundColor;
	}
	
	
    COLOR = newCol;
}