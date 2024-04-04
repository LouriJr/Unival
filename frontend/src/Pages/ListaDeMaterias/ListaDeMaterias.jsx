import React, { useEffect, useState } from 'react'
import ApiService from '../../Services/ApiService';
import ModalCadastroMateria from '../../Components/ModalCadastroMateria/ModalCadastroMateria';
import AuthService from '../../Services/AuthService';
import { useNavigate } from 'react-router-dom';
import ToastService from '../../Services/ToastService';

export default function ListaDeMaterias() {

    const navigate = useNavigate();

    const [materias, setMaterias] = useState([]);
    const [modalAberto, setModalAberto] = useState(false);

    function VerificarLogin() {
        const usuarioEstaLogado = AuthService.VerificarSeUsuarioEstaLogado();

        if (!usuarioEstaLogado) {
            navigate("/login");
        }
        BuscarMaterias();
    }

    async function BuscarMaterias() {
        try {
            const response = await ApiService.get('/Materias/listar');
            setMaterias(response.data);
        } catch (error) {
            ToastService.Error("Erro ao buscar matérias");
        }
    }

    useEffect(() => {
        VerificarLogin();
    }, []);

    function AbrirModal() {
        setModalAberto(true);
    }

    function Sair() {
        AuthService.Sair();
        navigate('/login');
    }

    return (
        <div>
            <ModalCadastroMateria
                modalAberto={modalAberto}
                setModalAberto={setModalAberto}
                materias={materias}
                buscarMaterias={BuscarMaterias}
            />
            <button onClick={AbrirModal}>Cadastrar nova materia</button>
            {
                materias.map(materia => (
                    <div>
                        <p>{materia.id}</p>
                        <p>{materia.nome}</p>
                        <p>{materia.descricao}</p>
                    </div>
                ))
            }
            
            <button onClick={Sair}>Sair</button>
        </div>
    )
}
