import { useEffect, useState } from "react";

export default function HomePage() {
  let contador = 0;
  const [count, setCount] = useState(0); // [valor, setarValor]
  console.log("Carregando tela com contador: ", count);

  const stateCount2 = useState();
  const count2 = stateCount2[0];
  const setCount2 = stateCount2[1];

  useEffect(() => {
    contador++;
    console.log("USE EFFECT VAZiO")
  }, []);

  useEffect(() => {
    contador++;
    console.log("USE EFFECT COUNT")
  }, [count]);

  const printValor = () => {
    alert(contador);
  }

  return (<>
    <h1>Olá, mundo! {contador}</h1>
    <h2>Subtitulo</h2>
    <h3>Mais um subtitulo</h3>
    <button onClick={() => setCount(count + 1)}>Contador {count}</button>
    <a href="./pagina2.html">Página 2</a>
    <a href="./cursos.html">Cursos</a>
    <button onClick={printValor}>Print valor</button>

    <div>
      <p>Esse é um paragrafo</p>
      <a href="https://unimar.edu.br" target="_blank">Site da Unimar</a>
      <Formulario></Formulario>
      <Formulario></Formulario>
      <Formulario></Formulario>
    </div>
    <div>
      <ol>
        <li>Item 1</li>
        <li>Item 2</li>
        <li>Item 3</li>
      </ol>
      <ul>
        <li>Item A</li>
        <li>Item B</li>
        <li>Item C</li>

      </ul>
    </div>
    <div>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Idade</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>1</td>
            <td>Rodrigo teste</td>
            <td>30</td>
          </tr>
          <tr>
            <td>2</td>
            <td>Fulano de tal</td>
            <td>20</td>
          </tr>
        </tbody>
      </table>
    </div >
  </>
  )
}

export function Formulario() {
  return (<form>
    <label>Nome:</label><input type="text" />
    <label>Email:</label><input type="email" />
    <label>Cor:</label><input type="color" />
    <label>Data:</label><input type="date" />
    <label>Idade:</label><input type="number" />
    <label>Arquivo:</label><input type="file" />
    <button type="submit">Enviar</button>
  </form>)
}