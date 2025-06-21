
// PascalCase
// camelCase

var contador = 1;
var currentId = null;

function enviarFormCurso(event) {
    event.preventDefault();

    const form = event.target;
    const formData = new FormData(form);

    const curso = {
        nome: formData.get("nome"),
        nivel: Number(formData.get("nivel")),
        descricao: formData.get("descricao")
    };
    // falsey values: 0, null, undefined, "", false
    // not falsey values = TRUE
    if (currentId) {
        curso.id = currentId;
    }

    const tabela = document.getElementById("tabela-cursos");

    const tbody = tabela.querySelector("tbody");

    //confirmar se usuario deseja realmente cadastrar

    const pedeConfirmacao = new Promise((resolve) => {
        setTimeout(() => {
            resolve(true)
        }, 2000)
    });
    console.log("asdasdas")
    pedeConfirmacao.then(async (confirmou) => {
        if (confirmou) {
            const resultado = await fetch(`${URL_API}/api/curso`, {
                method: currentId ? "PUT" : "POST",
                body: JSON.stringify(curso),
                headers: {
                    "Content-type": "application/json"
                }
            });
            var dados = await resultado.json();
            if (resultado.status == 200) {
                tbody.innerHTML = "";
                listarCursos();
                form.reset();
            }
        } else {

        }
    });
    console.log("asdasdas")
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

function adicionarEventoEdicao() {
    const tabela = document.getElementById("tabela-cursos");

    const tbody = tabela.querySelector("tbody");

    tbody.addEventListener("click", async (event) => {
        const linha = event.target.closest("tr");
        const colunaID = linha.querySelector("td:first-child");
        const id = colunaID.innerText.trim();

        const resultado = await fetch(`${URL_API}/api/curso/${id}`);
        if (resultado.status == 200) {
            const cursoDados = await resultado.json();
            abrirFormulario(cursoDados);
        }
    });
}

function abrirFormulario(curso) {
    currentId = curso.id;
    const form = document.querySelector("form");
    form.querySelector("input[name=nome]").value = curso.nome;
    form.querySelector("textarea[name=descricao]").value = curso.descricao;
    form.querySelector("select[name=nivel]").value = curso.nivel;

    form.parentElement.classList.remove("hidden");
}

function limparForm() {
    const form = document.querySelector("form");
    form.reset();
    currentId = null;

    form.parentElement.classList.remove("hidden");
}
