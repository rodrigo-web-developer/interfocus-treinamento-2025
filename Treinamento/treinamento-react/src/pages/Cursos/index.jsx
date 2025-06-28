import { useEffect, useState } from "react"
import { getCursoById, listarCursos, salvarCurso } from "../../services/cursoService";
import Modal from "../../components/Modal";
import { useRouter } from "simple-react-routing"

export default function CursosPage() {

    const [cursos, setCursos] = useState([]);

    const { pathParams } = useRouter();

    const [open, setOpen] = useState(false);

    const [selected, setSelected] = useState(null);

    const params = new URLSearchParams(window.location.search);

    const [search, setSearch] = useState(params.get("q"));

    const fetchData = async () => {
        const resultado = await listarCursos(search);
        if (resultado.status == 200) {
            setCursos(resultado.data);
        }
    }

    const submitForm = async (event) => {
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
        if (selected?.id) {
            curso.id = selected.id;
        }
        const resultado = await salvarCurso(curso);
        if (resultado.status == 200) {
            setOpen(false);
            fetchData();
        }
    }

    useEffect(() => {
        if (pathParams["id"]) {
            selecionarLinha({ id: pathParams["id"] })
        }
    }, [])

    useEffect(() => {
        fetchData();
    }, [search]);

    const selecionarLinha = async (curso) => {
        const resultado = await getCursoById(curso.id);
        if (resultado.status == 200) {
            setSelected(resultado.data);
            setOpen(true);
        }
    };

    return (<>
        <h1>CURSOS: {search}</h1>

        <div className="row">
            <label>Pesquisa:</label>
            <input name="pesquisa"
                type="search"
                value={search}
                onChange={(e) =>
                    setSearch(e.target.value)
                }
            />
            <button type="button" onClick={() => {
                setOpen(true);
                setSelected(null);
            }}>Novo Curso</button>
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
                {
                    cursos.map(curso =>
                        <LinhaCurso key={curso.id}
                            curso={curso}
                            onClick={() => selecionarLinha(curso)}></LinhaCurso>
                    )
                }
            </tbody>
        </table>

        <Modal open={open}>
            <form method="post" onSubmit={submitForm}>
                <label htmlFor="curso-nome">Nome:</label>
                <input defaultValue={selected?.nome} id="curso-nome" required minLength="10" maxLength="50" name="nome" type="text" />
                <label>Nível:</label>
                <select defaultValue={selected?.nivel} required name="nivel">
                    <option value="0">Iniciante</option>
                    <option value="1">Intermediário</option>
                    <option value="2">Avançado</option>
                </select>
                <label>Descrição:</label>
                <textarea defaultValue={selected?.descricao} required name="descricao"></textarea>
                <div className="row">
                    <button type="reset" onClick={() => setOpen(false)}>Cancelar</button>
                    <button type="submit">Cadastrar</button>
                </div>
            </form>
        </Modal>

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

const niveis = [
    "Iniciante",
    "Intermediário",
    "Avançado"
]

function LinhaCurso({ curso, onClick }) {
    const data = new Date(curso.dataCadastro);

    return (<tr onClick={onClick}>
        <td>{curso.id}</td>
        <td>{curso.nome}</td>
        <td>{niveis[curso.nivel]}</td>
        <td>{data.toLocaleString()}</td>
    </tr>)
}