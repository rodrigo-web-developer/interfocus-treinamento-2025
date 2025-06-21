
// PascalCase
// camelCase

var contador = 1;
var currentId = null;

export async function salvarCurso(curso) {
    const resultado = await fetch(`${URL_API}/api/curso`, {
        method: curso.id ? "PUT" : "POST",
        body: JSON.stringify(curso),
        headers: {
            "Content-type": "application/json"
        }
    });
    var dados = await resultado.json();
    return {
        status: resultado.status,
        data: dados
    }
}

const URL_API = "http://localhost:5188";

export function listarCursos() {
    return fetch(`${URL_API}/api/curso`, {
        method: "GET"
    }).then(async resultado => {
        if (resultado.status === 200) {
            const data = await resultado.json();
            return {
                status: resultado.status,
                data: data
            }
        }
        return {
            status: resultado.status,
            data: null
        }
    })
}

export async function getCursoById(id) {
    const resultado = await fetch(`${URL_API}/api/curso/${id}`, {
        method: "GET"
    });
    var dados = await resultado.json();
    return {
        status: resultado.status,
        data: dados
    }
}
