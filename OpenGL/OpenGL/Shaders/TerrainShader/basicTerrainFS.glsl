#version 400 core

 
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


in vec3 passed_normal;
in vec3 FragPos;
in vec2 passed_uv;

out vec4 outColor;

 
uniform DirectionalLight DirectionalLights[4];
uniform PointLight PointLights[4];
uniform SpotLight SpotLights[4];
uniform vec3 camPos;
uniform sampler2D background;
uniform sampler2D rTexture;
uniform sampler2D gTexture;
uniform sampler2D bTexture;
uniform sampler2D blendMap;

const float Shininess = 32 ;

vec4 GetColorFromDirectionalLight(DirectionalLight light , vec3 color){

	
	vec3 ambient = color * light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(-light.Direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  color;

	//specular
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) ,Shininess);
	vec3 specular = light.Specular * spec * color;

	vec3 result = ambient + diffuse + specular;

	vec4 outColor = vec4(result ,1.0);
	return outColor;
}

vec4 GetColorFromPointLight(PointLight light ,vec3 color){

	//ambient general light doesn't have any particular origin or direction
	vec3 ambient = color * light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(light.Position -FragPos);
	//vec3 lightDir = normalize(-light.direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  color;

	//specular
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) , Shininess);
	vec3 specular = light.Specular * spec * color;

	//attenuation
	float distance  = length(light.Position - FragPos);
	float attenuation =1.0f/ (light.Constant  + light.Linear * distance  + light.Quadratic *( distance *distance)); 
	
	ambient *= attenuation;
	diffuse *= attenuation;
	specular *= attenuation;
	
	vec3 result = ambient + diffuse + specular;

    vec4 outColor =  vec4(result ,1.0);

	return outColor;
}

vec4 GetColorFromSpotLight(SpotLight light , vec3 color){

	vec3 ambient = color * light.Ambient;

	//diffuse takes into consideration position and direction of normal at a point
	vec3 norm = normalize(passed_normal);
	vec3 lightDir = normalize(light.Position -FragPos);
	//vec3 lightDir = normalize(-light.direction);
	float diff = max(0.0,dot(norm ,lightDir));
	vec3 diffuse= light.Diffuse * diff *  color;

	//specular
	float specularStrength =2;  //factors that should be sent 0.5 try 2 try 10 
	vec3 viewDir = normalize(camPos - FragPos);
	vec3 reflectDir = reflect(-lightDir , norm);
	float spec = pow(max(dot(viewDir ,reflectDir) , 0.0) , Shininess);
	vec3 specular = light.Specular * spec * color;

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

	return outColor;
}


void main(){


	//============ using textures========================= 
	vec4 blendMapColor = texture(blendMap ,passed_uv);

	float beackGroundTexAmout =  1 -(blendMapColor.r + blendMapColor.g +blendMapColor.b);
	vec2 tiledCoords= passed_uv  * 40.0;
	vec4 backgroundTextureColor =texture(background , tiledCoords) * beackGroundTexAmout;
	vec4 rTextureColor =texture(rTexture ,tiledCoords) * blendMapColor.r;	
	vec4 gTextureColor =texture(gTexture ,tiledCoords) * blendMapColor.g;	
	vec4 bTextureColor =texture(bTexture ,tiledCoords) * blendMapColor.b;	

	vec3 totalColor = vec3(backgroundTextureColor +rTextureColor +gTextureColor + bTextureColor);

	 vec4 total =vec4(0.0);
	 
	 for(int i=0; i<4; i++){
	 
		total  = total + GetColorFromDirectionalLight(DirectionalLights[i] , totalColor) +
			GetColorFromPointLight(PointLights[i] , totalColor) +
			GetColorFromSpotLight(SpotLights[i] , totalColor);
	 
	 }

	 outColor = total;
}