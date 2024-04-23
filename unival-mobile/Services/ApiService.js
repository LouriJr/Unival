import axios from "axios";
import AuthService from "./AuthService";

async function createHeader() {
    const jwt = await AuthService.PegarToken();
    return {
        headers: {
            Authorization: 'Bearer ' + jwt
        }
    }
}

const baseUrl = "https://localhost:7007/api"
const ApiService = {

    async get(endpoint) {
        const headers = await createHeader();
        console.log(headers);

        const response = await axios.get(baseUrl + endpoint, headers);
        return response;
    },

    async post(endpoint, body) {
        const header = await createHeader();

        const response = await axios.post(baseUrl + endpoint, body, header);
        return response;
    }
};

export default ApiService;