import React, { useState, useEffect, useCallback } from 'react';
import HomePage, { Formulario } from './pages/Home';
import CursosPage from './pages/Cursos';
import { Layout } from './components/Layout';
import { BrowserRouter } from 'simple-react-routing';
import AlunosPage from './pages/Alunos';

class Aluno { }

const array = [10, 12];

const primeiroItem = array[0];
const segundoItem = array[1];

const [primeiro, segundo] = [10, 12];

const aluno = {
  nome: "rodrigo",
  email: "email",
  idade: 10
};

const nomeAluno = aluno.nome;
const idadeAluno = aluno.idade;

const { nome, idade } = aluno;

function App() {

  return <BrowserRouter routes={[{
    path: "",
    component: <HomePage></HomePage>
  }, {
    path: "cursos",
    component: <CursosPage></CursosPage>
  }, {
    path: "alunos",
    component: <AlunosPage></AlunosPage>
  }]}>
    <Layout></Layout>
  </BrowserRouter>
}


export default App;
