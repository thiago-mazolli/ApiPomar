## :bookmark: ApiPomar
API CRUD para gerencimaneto de um Pomar de um produtor de frutas.

## :rocket: Tecnologias

- C#

- BD Oracle - PL/SQL

## :bookmark: Aplicação

A API está configurada e publicada em ambiente Mega Cloud e pode ser acessada através do link abaixo:

**http://192.168.0.52/ApiPomar**

<hr />

Métodos de consulta **(GET)**:

**http://192.168.0.52/ApiPomar/Especie**

**http://192.168.0.52/ApiPomar/Especie/1**

**http://192.168.0.52/ApiPomar/Arvore**

**http://192.168.0.52/ApiPomar/Arvore/1**

**http://192.168.0.52/ApiPomar/Grupo**

**http://192.168.0.52/ApiPomar/Grupo/1**

**http://192.168.0.52/ApiPomar/Colheita**

**http://192.168.0.52/ApiPomar/Colheita/1**

**http://192.168.0.52/ApiPomar/Colheita/Data/31-01-2021**

<hr />

Métodos de consulta **(POST)**, abaixo exemplos de chamada e corpo da requisição:

**http://localhost:61304/ApiPomar/Especie**

{
	"Descricao": "Espécie 1"
}


**http://192.168.0.52/ApiPomar/Arvore**

{
	"Descricao": "Árvore 1",
	"Idade": 10,
	"Especie": {
		"Codigo": 1
	}
}


**http://192.168.0.52/ApiPomar/Grupo**

{
	"Descricao": "Grupo 1",
	"Arvores": [
		{
			"Codigo": 1
		}
	]
}


**http://192.168.0.52/ApiPomar/Colheita**

{
	"Data": "31/01/2021",
	"Peso": 5.5,
	"Arvore": {
		"Codigo": 1
	}
}

<hr />

Métodos de exclusão **(DELETE)**:

**http://192.168.0.52/ApiPomar/Colheita/1**

**http://192.168.0.52/ApiPomar/Grupo/1**

**http://192.168.0.52/ApiPomar/Arvore/1**

**http://192.168.0.52/ApiPomar/Especie/1**

<hr />

Métodos de alteração **(PUT)**:

**http://192.168.0.52/ApiPomar/Colheita/1**

{
	"Data": "31/01/2021",
	"Peso": 10,
	"Arvore": {
		"Codigo": 1
	}
}


**http://192.168.0.52/ApiPomar/Grupo/1**

{
	"Descricao": "Grupo Teste",
	"Arvores": [
		{
			"Codigo": 1
		},
    {
			"Codigo": 2
		}
	]
}


**http://192.168.0.52/ApiPomar/Arvore/1**

{
	"Descricao": "Árvore Teste",
	"Idade": 10,
	"Especie": {
		"Codigo": 2
	}
}


**http://192.168.0.52/ApiPomar/Especie/1**

{
	"Descricao": "Espécie Teste"
}
