#version 400 core

layout (location = 0) in vec4 vertex_position;
layout (location = 1) in vec4 vertex_color;
//TODO: textures

uniform mat4 model_transform;
uniform mat4 camera_transform;
uniform mat4 camera_projection;

out vec4 frag_color;

void main()
{
	mat4 model_view_projection = camera_projection * camera_transform * model_transform;
	//gl_Position = (model_transform * camera_transform * camera_projection) * vertex_position;
	gl_Position = model_view_projection * vertex_position;
    frag_color = vertex_color;
}
