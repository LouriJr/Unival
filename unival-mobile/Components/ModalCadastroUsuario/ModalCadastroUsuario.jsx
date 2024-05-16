import React, { useState } from 'react'
import { Modal, StyleSheet, Text, TextInput, TouchableOpacity, View, Image } from 'react-native'
import ToastService from '../../Services/ToastService';
import ApiService from '../../Services/ApiService';
import * as ImagePicker from 'expo-image-picker';

export default function ModalCadastroUsuario({ isVisible, onClose }) {

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [base64, setBase64] = useState("");

    async function Cadastrar() {
        try {
            const body = {
                email,
                senha,
                base64
            };

            await ApiService.post("/usuarios/cadastrar", body);
            ToastService.Success("Usuário cadastrado com sucesso!");
            onClose(false);

        } catch (error) {
            ToastService.Error("Erro ao cadastrar usuário");
        }
    }

    async function selecionarImagem() {
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Images,
            quality: 1,
        });

        if (result.canceled) {
            return;
        }
        setBase64(result.assets[0].uri);
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

                <Image source={{ uri: base64 }} style={{ width: 200, height: 200 }} />
                <TouchableOpacity onPress={selecionarImagem}>
                    <Text style={styles.text} >Adicionar Imagem</Text>
                </TouchableOpacity>
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