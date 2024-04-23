import React, { useEffect, useState } from 'react'
import { Text, TextInput, TouchableOpacity, View } from 'react-native'
import ApiService from '../../Services/ApiService';
import AuthService from '../../Services/AuthService';
import ToastService from '../../Services/ToastService';
import ModalCadastroUsuario from '../../Components/ModalCadastroUsuario/ModalCadastroUsuario';
import { useNavigation } from "@react-navigation/native";

export default function Login() {

    const navigation = useNavigation();

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [isModalVisible, setIsModalVisible] = useState(false);

    useEffect(() => {
        VerificarLogin();
    }, [])


    async function VerificarLogin() {
        const usuarioEstaLogado = await AuthService.VerificarSeUsuarioEstaLogado();

        if (usuarioEstaLogado) {
            navigation.navigate("Home");
        }
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
            navigation.navigate("Home");
        }
        catch (error) {
            if (error.response?.status === 401) {
                ToastService.Error("E-mail e/ou senha inválidos!");
                return;
            }
            ToastService.Error("Houve um erro no servidor ao realizar o seu login\r\nTente novamente mais tarde.");
        }
    }

    function AbrirModal() {
        setIsModalVisible(true);
    }

    return (
        <View>
            <ModalCadastroUsuario isVisible={isModalVisible} onClose={() => setIsModalVisible(false)} />
            <TextInput
                placeholder="Digite o e-mail"
                value={email}
                onChangeText={(texto) => setEmail(texto)}
            />
            <TextInput
                placeholder="Senha"
                value={senha}
                secureTextEntry={true}
                onChangeText={(texto) => setSenha(texto)}
            />
            <TouchableOpacity onPress={Login}>
                <Text>Entrar</Text>
            </TouchableOpacity>

            <TouchableOpacity onPress={AbrirModal}>
                <Text>Cadastrar Usuário</Text>
            </TouchableOpacity>
        </View>
    )
}
