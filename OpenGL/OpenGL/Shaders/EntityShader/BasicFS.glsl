#version 400 core

struct Material
{
	sampler2D Diffuse;
	sampler2D Specular;
	float Shininess;
};

struct DirectionalLight{
	vec3 Direction;
    vec3 Ambient;
    vec3 Diffuse;
    vec3 Specular;
};

struct PointLight
{	
	vec3 Position;
	vec3 Ambient;
	vec3 Diffuse;
	vec3 Specular;
	float Constant;
	float Linear;
	float Quadratic;
};

struct SpotLight{
	vec3 Position;
	vec3 Direction;
	vec3 Ambient;
	vec3 Diffuse;
	vec3 Specular;
	float Constant;
	float Linear;
	float Quadratic;
	float CutOff;
	float OuterCutOff;
};

#define MAX_LIGHTS 4
#define MAX_MATERIALS 20

in vec3 passed_normal;
in vec3 FragPos;
in vec2 passed_uv;

out vec4 outColor;


uniform Material Materials[MAX_MATERIALS];
uniform DirectionalLight DirectionalLights[MAX_LIGHTS];
uniform PointLight PointLights[MAX_LIGHTS];
uniform SpotLight SpotLights[MAX_LIGHTS];
uniform vec3 camPos;


vec4 GetColorFromDirectionalLight( DirectionalLight light , Material material);
vec4 GetColorFromPointLight( PointLight light , Material material);
vec4 GetColorFromSpotLight( SpotLight light , Material material);




void main(){
 
	vec4 total = vec4(0.0);

	for(int i=0; i<4; i++){
		
			for(int j=0; j<20 ;j++){
				total = total + GetColorFromDirectionalLight(DirectionalLights[i] ,Materials[j]) + 
				GetColorFromPointLight(PointLights[i] , Materials[j])  + 
				GetColorFromSpotLight(SpotLights[i] , Materials[j]);
			}
		}

	outColor = total;
}



vec4 GetColorFromDirectionalLight( DirectionalLight light , Material material){

 
	vec3 ambient = vec3(texture( material.Diffuse , passed_uv))* light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(-light.Direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  vec3(texture(material.Diffuse, passed_uv));

	//specular
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) , material.Shininess);
	vec3 specular = light.Specular * spec * vec3(texture(material.Specular ,passed_uv));

	vec3 result = ambient + diffuse + specular;

	vec4 outColor = vec4(result ,1.0);
	return (outColor);
}

vec4 GetColorFromPointLight( PointLight light , Material material){

	//ambient general light doesn't have any particular origin or direction
	vec3 ambient = vec3(texture( material.Diffuse , passed_uv))* light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(light.Position -FragPos);
	//vec3 lightDir = normalize(-light.direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  vec3(texture(material.Diffuse , passed_uv));

	//specular
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) , material.Shininess);
	vec3 specular = light.Specular * spec * vec3(texture(material.Specular ,passed_uv));

	//attenuation
	float distance  = length(light.Position - FragPos);
	float attenuation =1.0f/ (light.Constant  + light.Linear * distance  + light.Quadratic *( distance *distance)); 
	
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;
	
	vec3 result = ambient + diffuse + specular;

    vec4 outColor =  vec4(result ,1.0);

	return (outColor);
}

vec4 GetColorFromSpotLight( SpotLight light , Material material){

	vec3 ambient = vec3(texture( material.Diffuse , passed_uv))* light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(light.Position -FragPos);
	//vec3 lightDir = normalize(-light.direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  vec3(texture(material.Diffuse , passed_uv));

	//specular
	float specularStrength =2;  //factors that should be sent 0.5 try 2 try 10 
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) , material.Shininess);
	vec3 specular = light.Specular * spec * vec3(texture(material.Specular ,passed_uv));

	 //spot light
	float theta = dot(lightDir ,normalize(-light.Direction)); 
	float epsilon = (light.CutOff - light.OuterCutOff);
	float intensity = clamp((theta - light.OuterCutOff)/epsilon ,0.0,1.0);

	diffuse *= intensity;
	specular *= intensity;

	//attenuation
	float Distance  = length(light.Position - FragPos);
	float attenuation =1.0f/ (light.Constant  + light.Linear * Distance  + light.Quadratic *( Distance *Distance)); 
	
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;
	
	vec3 result = ambient + diffuse + specular;
	
	vec4 outColor =  vec4(result ,1.0);

	return (outColor);
}
