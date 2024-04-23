import React, { useEffect, useState } from 'react'
import { Text, TouchableOpacity, View } from 'react-native'
import AuthService from '../../Services/AuthService';
import ApiService from '../../Services/ApiService';
import ToastService from '../../Services/ToastService';
import { useNavigation } from "@react-navigation/native";

export default function Home() {

    const navigation = useNavigation();
    const [materias, setMaterias] = useState([]);

    async function VerificarLogin() {
        const usuarioEstaLogado = AuthService.VerificarSeUsuarioEstaLogado();

        if (!usuarioEstaLogado) {
            navigate("/login");
        }
        await BuscarMaterias();
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

    async function Sair() {
        await AuthService.Sair();
        navigation.navigate('Login');
    }

    return (
        <View>
            {
                materias.map(materia => (
                    <View>
                        <Text>{materia.id}</Text>
                        <Text>{materia.nome}</Text>
                    </View>
                ))
            }
            <TouchableOpacity onPress={Sair}>
                <Text>Logout</Text>
            </TouchableOpacity>
        </View>
    )
}
