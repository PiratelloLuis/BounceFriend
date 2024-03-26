shader_type canvas_item;

uniform float move_speed = -0.008;

void fragment() {
	COLOR = texture(TEXTURE, vec2(UV.x + TIME * move_speed, UV.y));
}
