import React, { useEffect, useState } from 'react'
import ApiService from '../../Services/ApiService';
import AuthService from '../../Services/AuthService';
import { useNavigate } from "react-router-dom";
import ToastService from '../../Services/ToastService';
import ModalCadastroUsuario from '../../Components/ModalCadastroUsuario/ModalCadastroUsuario';

export default function Login() {

    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [modalAberto, setModalAberto] = useState(false);

    useEffect(() => {
        VerificarLogin();
    }, []);

    function VerificarLogin() {
        const usuarioEstaLogado = AuthService.VerificarSeUsuarioEstaLogado();
        if (usuarioEstaLogado) {
            navigate("/");
        }
    }

    function AbrirModal() {
        setModalAberto(true);
    }

    async function Login() {
        try {
            const body = new URLSearchParams({
                email,
                senha,
            });

            const response = await ApiService.post("/Usuarios/Login", body);
            const token = response.data.token;

            AuthService.SalvarToken(token);

            ToastService.Success("Seja bem vindo, " + email);

            navigate("/");
        }
        catch (error) {
            if (error.response?.status === 401) {
                ToastService.Error("E-mail e/ou senha inválidos!");
                return;
            }
            ToastService.Error("Houve um erro no servidor ao realizar o seu login\r\nTente novamente mais tarde.");
        }
    }

    return (
        <div>
            <ModalCadastroUsuario
                modalAberto={modalAberto}
                setModalAberto={setModalAberto}
            />
            <div>
                <span>Login</span>
                <input
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder='E-mail' />

                <input
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                    placeholder='Senha'
                    type='Password' />

                <button onClick={Login}>Login</button>
            </div>
            <div>
                <button onClick={AbrirModal}>Novo por aqui? Cadastre-se</button>
            </div>

        </div>
    )
}
