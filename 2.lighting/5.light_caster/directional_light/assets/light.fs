#version 330 core
struct Material
{
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};
uniform Material material;
struct Light
{
    //vec3 position;
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
uniform Light light;
in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;
out vec4 color;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
	
    // 环境光

    // 漫反射光
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(-light.direction);
    float diff = max(dot(norm, lightDir), 0.0);

    // 镜面高光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
    color = vec4(ambient + diffuse + specular, 1.0f);
}
