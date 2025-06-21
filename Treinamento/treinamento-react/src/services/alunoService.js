
// PascalCase
// camelCase

export async function salvarAluno(aluno) {
    const resultado = await fetch(`${URL_API}/api/aluno`, {
        method: aluno.id ? "PUT" : "POST",
        body: JSON.stringify(aluno),
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

export function listarAlunos(busca) {
    return fetch(`${URL_API}/api/aluno?pesquisa=${busca || ""}`, {
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

export async function getAlunoById(id) {
    const resultado = await fetch(`${URL_API}/api/aluno/${id}`, {
        method: "GET"
    });
    var dados = await resultado.json();
    return {
        status: resultado.status,
        data: dados
    }
}
