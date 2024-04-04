import axios from "axios";
import AuthService from "./AuthService";

function createHeader() {
    const jwt = AuthService.PegarToken();
    if (jwt) {
        return {
            headers: {
                Authorization: 'Bearer ' + jwt
            }
        }
    }
}

const baseUrl = "https://localhost:7007/api"
const ApiService = {

    async get(endpoint) {
        const headers = createHeader();

        const response = await axios.get(baseUrl + endpoint, headers);
        return response;
    },

    async post(endpoint, body) {
        const header = createHeader();

        const response = await axios.post(baseUrl + endpoint, body, header);
        return response;
    }
};

export default ApiService;