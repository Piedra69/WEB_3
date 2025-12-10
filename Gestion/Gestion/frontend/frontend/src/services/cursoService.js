import axios from "axios";

const API_URL = "https://localhost:7227/api/Curso";

export default {
    obtener() {
        return axios.get(`${API_URL}/obtener`);
    },

    registrar(data) {
        return axios.post(`${API_URL}/registrar`, data);
    },

    actualizar(data) {
        return axios.put(`${API_URL}/update`, data);
    },

    eliminar(id) {
        return axios.delete(`${API_URL}/delete?id=${id}`);
    }
};
