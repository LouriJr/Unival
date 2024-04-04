import React, { useEffect, useState } from 'react'
import Modal from 'react-modal';
import ApiService from '../../Services/ApiService';
import Multiselect from 'multiselect-react-dropdown';
import ToastService from '../../Services/ToastService';

export default function ModalCadastroMateria({ modalAberto, setModalAberto, materias, buscarMaterias }) {
    const customStyles = {
        content: {
            top: '50%',
            left: '50%',
            right: 'auto',
            bottom: 'auto',
            marginRight: '-50%',
            transform: 'translate(-50%, -50%)',
        },
    };
    Modal.setAppElement('#root');

    const [dependenciasSelecionadas, setDependenciasSelecionadas] = useState([]);

    const [professores, setProfessores] = useState([]);
    const [idProfessorSelecionado, setIdProfessorSelecionado] = useState('');

    const [nome, setNome] = useState("");
    const [descricao, setDescricao] = useState("");

    async function Cadastrar() {
        try {
            const body = {
                nome,
                descricao,
                professor: {
                    id: idProfessorSelecionado
                },
                dependencias: dependenciasSelecionadas
            };

            await ApiService.post("/materias", body);

            setModalAberto(false);
            ToastService.Success("Matéria cadastrada com sucesso");
            await buscarMaterias();

        } catch (error) {
            ToastService.Error("Erro ao cadastrar matéria");
        }
    }

    async function BuscarProfessores() {
        try {
            const response = await ApiService.get('/professores');
            setProfessores(response.data);
        } catch (error) {
            ToastService.Error("Erro ao listar professores");
        }
    }

    useEffect(() => {
        BuscarProfessores();
    }, []);

    function FecharModal() {
        setModalAberto(false)
    }

    function selectAlterado(event) {
        setIdProfessorSelecionado(event.target.value);
    }

    function quandoSelecionarDependencia(selectedList, dependencia) {
        const listaDeDependencias = dependenciasSelecionadas;
        listaDeDependencias.push(dependencia);

        setDependenciasSelecionadas(listaDeDependencias);
    }

    function quandoRemoverDependencia(selectedList, dependencia) {
        let listaDeDependencias = dependenciasSelecionadas;
        listaDeDependencias = listaDeDependencias.filter(d => d.id != dependencia.id);

        setDependenciasSelecionadas(listaDeDependencias);
    }

    return (
        <Modal
            isOpen={modalAberto}
            style={customStyles}
            contentLabel="Example Modal"
            shouldCloseOnEsc={true}
            shouldCloseOnOverlayClick={true}
            onRequestClose={FecharModal}
        >
            <h2>Hello</h2>
            <button onClick={FecharModal}>Fechar</button>
            <input placeholder='Nome' value={nome} onChange={(e) => setNome(e.target.value)} />
            <input placeholder='Descricao' value={descricao} onChange={(e) => setDescricao(e.target.value)} />

            <select value={idProfessorSelecionado} onChange={selectAlterado}>
                <option value="" disabled>Selecione um professor</option>
                {professores.map((professor) => (
                    <option key={professor.id} value={professor.id}>{professor.nome}</option>
                ))}
            </select>


            <Multiselect
                options={materias}
                selectedValues={dependenciasSelecionadas}
                onSelect={quandoSelecionarDependencia}
                onRemove={quandoRemoverDependencia}
                displayValue="nome"
            />

            <button onClick={Cadastrar}>Cadastrar</button>
        </Modal>
    )
}
