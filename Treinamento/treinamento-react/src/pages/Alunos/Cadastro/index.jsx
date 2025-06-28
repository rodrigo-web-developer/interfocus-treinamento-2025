import { useNavigation, useRouter } from "simple-react-routing";
import { getAlunoById, salvarAluno } from "../../../services/alunoService";
import { useEffect, useState } from "react";

export default function AlunoCadastroPage() {

    const { pathParams } = useRouter();

    const { navigateTo } = useNavigation();

    const [current, setCurrent] = useState();

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

    useEffect(() => {
        if (pathParams["id"]) {
            getAlunoById(pathParams["id"])
                .then(response => {
                    if (response.status == 200) {
                        setCurrent(response.data);
                    }
                })
        } 
        else {
            setCurrent({});
        }
    }, [])

    return current && <>
        <h1>Cadastro de aluno</h1>

        <form className="column" onSubmit={submitForm}>
            <div className="column">
                <label>Nome:</label>
                <input defaultValue={current?.nome} name="nome" type="text" />
            </div>
            <div className="column">
                <label>E-mail:</label>
                <input defaultValue={current?.email} name="email" type="email" />
            </div>
            <div className="column">
                <label>Data de nascimento:</label>
                {/* YYYY-MM-DD */}
                <input defaultValue={new Date(current?.dataNascimento).toISOString().split('T')[0]} name="data-nascimento" type="date" />
            </div>
            <div className="row-end">
                <button type="reset">Cancelar</button>
                <button type="submit">Salvar</button>
            </div>
        </form>
    </>;
}