export default function CursosPage() {
    return (<>
        <nav>
            <ul>
                <li>
                    <a href="/">Home</a>
                </li>
                <li>
                    <a href="/cursos">Cursos</a>
                </li>
                <li>
                    <a href="/alunos">Alunos</a>
                </li>
            </ul>
        </nav>
        <h1>CURSOS</h1>

        <div className="row">
            <label>Pesquisa:</label>
            <input name="pesquisa" type="text" />
            <button type="button" onclick="limparForm()">Novo Curso</button>
        </div>

        <table id="tabela-cursos">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Nível</th>
                    <th>Data Cadastro</th>
                </tr>
            </thead>
            <tbody>

            </tbody>
        </table>

        <div className="modal hidden">
            <form method="post" onsubmit="enviarFormCurso(event)">
                <label for="curso-nome">Nome:</label>
                <input id="curso-nome" required minlength="10" maxlength="50" name="nome" type="text" />
                <label>Nível:</label>
                <select required name="nivel">
                    <option value="0">Iniciante</option>
                    <option value="1">Intermediário</option>
                    <option value="2">Avançado</option>
                </select>
                <label>Descrição:</label>
                <textarea required name="descricao"></textarea>
                <button type="submit">Cadastrar</button>
            </form>
        </div>

        <div>
            <h2>Subtitulo</h2>
            <p>texto de teste</p>
        </div>
        <div className="grid">
            <strong>Observação: </strong>
            <p>texto de teste</p>
        </div>
    </>)
}