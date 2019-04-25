#version 400 core


layout(location=0)in vec3 position;
layout(location=1)in vec2 uv;
layout(location=2)in vec3 normal;



uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 passed_normal;
out vec3 FragPos;
out vec2 passed_uv;
 
 void main(){
	gl_Position = projection * view * model * vec4(position,1.0);
	FragPos = vec3(model *vec4(position,1.0));
	passed_normal = mat3(transpose(inverse(model)))* normal;
	passed_uv =uv;
 }