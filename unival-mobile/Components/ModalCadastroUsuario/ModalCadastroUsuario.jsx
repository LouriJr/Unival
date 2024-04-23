import React, { useState } from 'react'
import { Modal, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native'
import ToastService from '../../Services/ToastService';
import ApiService from '../../Services/ApiService';

export default function ModalCadastroUsuario({ isVisible, onClose }) {

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    async function Cadastrar() {
        try {
            const body = {
                email,
                senha,
            };

            await ApiService.post("/usuarios/cadastrar", body);
            ToastService.Success("Usuário cadastrado com sucesso!");
            onClose(false);

        } catch (error) {
            ToastService.Error("Erro ao cadastrar usuário");
        }
    }

    return (
        <Modal animationType="slide" transparent={true} visible={isVisible}>
            <View style={styles.container} >
                <TextInput
                    placeholder="Digite o e-mail"
                    value={email}
                    onChangeText={(texto) => setEmail(texto)}
                    style={styles.text}
                />
                <TextInput
                    placeholder="Senha"
                    value={senha}
                    secureTextEntry={true}
                    onChangeText={(texto) => setSenha(texto)}
                    style={styles.text}
                />
                <TouchableOpacity onPress={Cadastrar}>
                    <Text style={styles.text} >Cadastrar</Text>
                </TouchableOpacity>
                <TouchableOpacity onPress={onClose}>
                    <Text style={styles.text}>Fechar</Text>
                </TouchableOpacity>
            </View>
        </Modal>
    )
}

const styles = StyleSheet.create({
    container: {
        height: '100%',
        width: '100%',
        backgroundColor: '#25292e',
        borderTopRightRadius: 18,
        borderTopLeftRadius: 18,
        position: 'absolute',
        bottom: 0,
        padding: 5,
    },
    text: {
        color: '#fff'
    }
});