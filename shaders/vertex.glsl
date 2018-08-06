#version 400 core

layout (location = 0) in vec4 vertex_position;
layout (location = 1) in vec4 vertex_color;

uniform mat4 model_view_projection;

out vec4 frag_color;

void main()
{
    gl_Position = model_view_projection * vertex_position;
    frag_color = vertex_color;
}
