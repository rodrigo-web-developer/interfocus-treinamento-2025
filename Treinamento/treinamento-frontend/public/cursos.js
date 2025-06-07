
// PascalCase
// camelCase

var contador = 1;

function enviarFormCurso(event) {
    event.preventDefault();

    const form = event.target;
    const formData = new FormData(form);

    const curso = {
        nome: formData.get("nome"),
        nivel: formData.get("nivel"),
        descricao: formData.get("descricao"),
        id: contador++,
        dataCadastro: new Date()
    };

    const campoInvalido = document
        .querySelector('form input[type="text"]:invalid');

    const tabela = document.getElementById("tabela-cursos");

    const tbody = tabela.querySelector("tbody");

    //confirmar se usuario deseja realmente cadastrar

    const pedeConfirmacao = new Promise((resolve) => {
        setTimeout(() => {
            resolve(true)
        }, 2000)
    });

    pedeConfirmacao.then(confirmou => {
        if (confirm) {
            tbody.innerHTML += `<tr>
                <td>${curso.id}</td>
                <td>${curso.nome}</td>
                <td>${curso.nivel}</td>
                <td>${curso.dataCadastro.toLocaleDateString()}</td>
            </tr>`
        } else {

        }
    })
}

const URL_API = "http://localhost:5188";

function listarCursos() {
    const response = fetch(`${URL_API}/api/curso`, {
        method: "GET"
    });

    response.then(resultado => {
        if (resultado.status === 200) {
            resultado.json().then(dados => {
                const tabela = document.getElementById("tabela-cursos");
                const tbody = tabela.querySelector("tbody");
                dados.forEach(curso => {
                    var novaLinha = document.createElement("tr");
                    var col1 = document.createElement("td");
                    var col2 = document.createElement("td");
                    var col3 = document.createElement("td");
                    var col4 = document.createElement("td");
                    col1.innerHTML = curso.id;
                    col2.innerHTML = curso.nome;
                    col3.innerHTML = curso.nivel;
                    col4.innerHTML = new Date(curso.dataCadastro).toLocaleString();
                    novaLinha.append(col1, col2, col3, col4);
                    tbody.appendChild(novaLinha);
                })
            })
        }
    })
}

setTimeout(() => {
    listarCursos();
}, 100);