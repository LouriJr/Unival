import AsyncStorage from "@react-native-async-storage/async-storage";
import { jwtDecode } from "jwt-decode";

const AuthService = {

    async SalvarToken(token) {
        await AsyncStorage.setItem("@jwt", token);
    },

    async PegarToken() {
        return await AsyncStorage.getItem("@jwt");
    },
    async Sair() {
        return await AsyncStorage.removeItem("@jwt");
    },

    async VerificarSeUsuarioEstaLogado() {
        const token = await AsyncStorage.getItem("@jwt");
        if (!token) { return false }

        const userData = jwtDecode(token);
        const dataAtual = Date.parse(new Date()) / 1000;

        if (dataAtual > userData.exp) {
            await AsyncStorage.removeItem("@jwt");
            return false;
        }

        return true;
    }
}

export default AuthService;