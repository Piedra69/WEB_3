import axios from "axios";

const API_URL = "https://localhost:7227/api/User";

export default {
    login(email, password) {
        return axios.post(`${API_URL}/login`, {
            email,
            password
        });
    }
};
