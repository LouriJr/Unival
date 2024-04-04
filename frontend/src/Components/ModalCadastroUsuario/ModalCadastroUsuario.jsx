import React, { useEffect, useState } from 'react'
import Modal from 'react-modal';
import ApiService from '../../Services/ApiService';
import ToastService from '../../Services/ToastService';

export default function ModalCadastroUsuario({ modalAberto, setModalAberto }) {
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

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    async function Cadastrar() {
        try {
            const body = {
                email,
                senha
            };

            await ApiService.post("/usuarios/cadastrar", body);
            ToastService.Success("Usuário cadastrado com sucesso!");
            setModalAberto(false);

        } catch (error) {
            ToastService.Error("Erro ao cadastrar usuário");
        }
    }

    return (
        <Modal
            isOpen={modalAberto}
            style={customStyles}
        >
            <h2>Cadastre-se</h2>
            <button onClick={() => { setModalAberto(false) }}>Fechar</button>
            <input placeholder='E-mail' value={email} onChange={(e) => setEmail(e.target.value)} />
            <input placeholder='Senha' type='Password' value={senha} onChange={(e) => setSenha(e.target.value)} />

            <button onClick={Cadastrar}>Cadastrar</button>
        </Modal>
    )
}
