import { useEffect, useState } from "react";
import { listarAlunos } from "../../services/alunoService";

export default function AlunosPage() {
    const [alunos, setAlunos] = useState([]);

    const [search, setSearch] = useState("");

    const fetchData = async () => {
        const resultado = await listarAlunos(search);
        if (resultado.status == 200) {
            setAlunos(resultado.data);
        }
    }

    useEffect(() => {
        fetchData();
    }, [search])

    return <div>
        <h1>Alunos</h1>
        <div className="row">
            <label>Pesquisa: </label>
            <input type="search"
                value={search}
                onChange={(e) =>
                    setSearch(e.target.value)
                } />
        </div>
        <div className="grid-cards">
            {alunos.map(a =>
                <AlunoCard key={a.id} aluno={a}></AlunoCard>)}
        </div>
    </div>
}

function AlunoCard({ aluno }) {
    return <div class="card">
        <h4>{aluno.nome}</h4>
        <ul>
            <li>
                <strong>ID:</strong> {aluno.id}
            </li>
            <li>
                <strong>Idade:</strong> {aluno.idade}
            </li>
            <li>
                <strong>E-mail:</strong> {aluno.email}
            </li>
            <li>
                <strong>Inscrições:</strong> {aluno.inscricoes?.length}
            </li>
        </ul>
    </div>
}