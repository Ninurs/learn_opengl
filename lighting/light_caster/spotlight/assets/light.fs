#version 330 core
struct Material
{
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light
{
    vec3 position;
    vec3 direction;//聚光的方向    
    float cutOff;
    float outerCutOff;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
   
    float constant;
    float linear;
    float quadratic;
};

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

out vec4 color;

uniform Material material;
uniform Light light;
uniform vec3 viewPos;

void main()
{	
    vec3 lightDir = normalize(light.position - FragPos);
    //检查是否在聚光范围内
    float theta = dot(lightDir, normalize(-light.direction));
    
    if (theta > light.cutOff)
    {
    //float epsilon = light.cutOff - light.outerCutOff;
    //float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);
    
    // 环境光
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
    
    // 漫反射光
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0.0);
    //vec3 diffuse = light.diffuse * intensity;
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));

    // 镜面高光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
    //vec3 specular = light.specular * intensity;

    // 衰减
    float distance = length(light.position - FragPos);
    float attenuation = 1.0f / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    diffuse *= attenuation;
    specular *= attenuation;
    
    color = vec4(ambient + diffuse + specular, 1.0f);
    }
    else
        color = vec4(light.ambient * vec3(texture(material.diffuse, TexCoords)), 1.0f);
}
