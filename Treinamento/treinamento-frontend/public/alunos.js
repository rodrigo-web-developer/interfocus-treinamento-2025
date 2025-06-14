
// PascalCase
// camelCase

var contador = 1;

function enviarFormAluno(event) {
    event.preventDefault();

    const form = event.target;
    const formData = new FormData(form);

    const Aluno = {
        nome: formData.get("nome"),
        nivel: formData.get("nivel"),
        descricao: formData.get("descricao"),
        id: contador++,
        dataCadastro: new Date()
    };

    const campoInvalido = document
        .querySelector('form input[type="text"]:invalid');

    const tabela = document.getElementById("tabela-alunos");

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
                <td>${Aluno.id}</td>
                <td>${Aluno.nome}</td>
                <td>${Aluno.email}</td>
                <td>${Aluno.dataCadastro.toLocaleDateString()}</td>
            </tr>`
        } else {

        }
    })
}

const URL_API = "http://localhost:5188";

function listarAlunos() {
    const response = fetch(`${URL_API}/api/Aluno`, {
        method: "GET"
    });

    response.then(resultado => {
        if (resultado.status === 200) {
            resultado.json().then(dados => {
                const tabela = document.getElementById("grid-alunos");
                dados.forEach(aluno => {
                    
                    tabela.innerHTML += `
                    <div class="card">
                        <h4>${aluno.nome}</h4>
                        <ul>
                            <li>
                                <strong>ID:</strong> ${aluno.id}
                            </li>
                            <li>
                                <strong>Idade:</strong> ${aluno.idade}
                            </li>
                            <li>
                                <strong>E-mail:</strong> ${aluno.email}
                            </li>
                        </ul>
                    </div>
                    `
                })
            })
        }
    })
}

setTimeout(() => {
    listarAlunos();
}, 100);