shader_type canvas_item;


uniform sampler2D bgTexture;
uniform float screenSizeX = 180.0;
uniform float screenSizeY = 320.0;
uniform float scale = 3.0;

float rand(float co){
   return fract(sin(co * 78.233) * 439758.5453);
}

void vertex() {
	
   VERTEX.xy *= scale;
   VERTEX.x -= (screenSizeX / 2.0 * (scale - 1.0));
   VERTEX.y -= (screenSizeY / 2.0 * (scale - 1.0));
	
}

void fragment() {
	
	vec4 heartCol = texture(TEXTURE, UV);
	
	if (heartCol.a == 0.0) {
		
	    // Fetch the texture's pixel
	    vec4 col = texture(bgTexture, FRAGCOORD.xy); 
		
		float newX = UV.x + TIME / 20.0;
		vec4 newCol = texture(bgTexture, vec2(newX, UV.y));
		
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
	else {
		COLOR = vec4(heartCol.rgb, 0.0);
	}

}