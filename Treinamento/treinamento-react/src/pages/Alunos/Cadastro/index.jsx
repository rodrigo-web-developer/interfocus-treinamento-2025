import { useNavigation, useRouter } from "simple-react-routing";
import { salvarAluno } from "../../../services/alunoService";

export default function AlunoCadastroPage() {

    const { navigateTo } = useNavigation();

    const submitForm = async (e) => {
        e.preventDefault();
        var formData = new FormData(e.target);
        const aluno = {
            nome: formData.get("nome"),
            email: formData.get("email"),
            dataNascimento: new Date(formData.get("data-nascimento")),
        }

        const response = await salvarAluno(aluno);
        if (response.status == 200) {
            navigateTo(e, "/alunos")
        }
        else {

        }
    }

    return <>
        <h1>Cadastro de aluno</h1>

        <form className="column" onSubmit={submitForm}>
            <div className="column">
                <label>Nome:</label>
                <input name="nome" type="text" />
            </div>
            <div className="column">
                <label>E-mail:</label>
                <input name="email" type="email" />
            </div>
            <div className="column">
                <label>Data de nascimento:</label>
                <input name="data-nascimento" type="date" />
            </div>
            <div className="row-end">
                <button type="reset">Cancelar</button>
                <button type="submit">Salvar</button>
            </div>
        </form>
    </>;
}