shader_type canvas_item;

uniform float height = 5.0;
uniform float frequency = 2.0;
uniform float speed = 3.0;

void vertex() {
	VERTEX.y += height * sin((frequency * UV.x) + (speed * TIME));	
}
