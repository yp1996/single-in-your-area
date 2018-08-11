shader_type canvas_item;

uniform float screenSizeX = 180.0;
uniform float screenSizeY = 320.0;
uniform float rectangleSize = 50.0;
uniform float t = 0.0;

float rand(float co){
   return fract(sin(co * 78.233) * 439758.5453);
}


void fragment() {
	
	vec4 col = COLOR;
	vec4 newCol = col;
	float midpoint = screenSizeY / 2.0;
	float rectangleHalf = rectangleSize / 2.0;
	vec3 diffFromBlack = col.rgb; 
	
	if (t <= 0.2) {
		newCol.rgb -= diffFromBlack * (t / 0.2);
	}
	else {
		float rectangleProgress = (1.0 - (t-0.2)/0.8);
		float upperBound = midpoint + rectangleHalf * rectangleProgress;
		float lowerBound = midpoint + rectangleHalf * rectangleProgress;
		if (FRAGCOORD.y <= upperBound || FRAGCOORD.y >= lowerBound) {
			newCol = vec4(1.0, 1.0, 1.0, 1.0);
		} else {
			newCol = vec4(0.0, 0.0, 0.0, 0.0);
		}
	}
	col = newCol;
	

}