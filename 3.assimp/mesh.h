//网格类
#include <GL/glew>

#include <iostream>
#include <string>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

//Vertex结构体，索引顶点属性
struct Vertex
{
	//顶点位置向量
	glm::vec3 Position;
	//顶点法向量
	glm::vec3 Normal;
	//顶点纹理坐标
	glm::vec2 TexCoords;
};

//Texture结构体，储存纹理id和纹理类型(diffuse/specular)
struct Texture
{
	GLuint id;
	string type;
};

//网格类
class Mesh
{
Public:
	//网格数据
	vector<Vertex> vertices;
	vector<GLuint> indices;
	vector<Texture> textures;
	//函数
	Mesh(vector<Vertex> vertices, vector<GLuint> indices, vector<Texture> textures);
	//绘制网格
	void Draw(Shader, shader);
	
private:
	//渲染数据
	GLunit VAO, VBO, EBO;
	//函数，初始化缓冲
	void setupMesh();
}

//构造函数
Mesh(vector<Vertex> vertices, vector<GLuint> indices, vector<Texture> textures)
{
	this->vertices = vertices;
	this->indices = indices;
	this->textures = textures;

	this->setupMesh();
}

void setupMesh()
{
	
}
