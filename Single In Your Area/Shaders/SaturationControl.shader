shader_type canvas_item;
uniform float saturation;

void fragment() {
	
	float Pr = 0.299;
	float Pg = 0.587;
    float Pb = 0.114;

    // Fetch the texture's pixel
    vec4 col = texture(TEXTURE, UV);
	
	float  P=sqrt(col.r*col.r*Pr+col.g*col.g*Pg+col.b*col.b*Pb);
	col.r =P+((col.r)-P)*saturation;
    col.g = P+((col.g)-P)*saturation;
    col.b = P+((col.b)-P)*saturation; 

    // Assign the color to the output
    COLOR = col;
}