import { createRouter, createWebHistory } from "vue-router";
import Login from "../components/Login.vue";
import Dashboard from "../components/Dashboard.vue";
import CursoList from "../components/Curso/CursoList.vue";


const routes = [
    { path: "/", component: Login },
    { path: "/cursos", component: CursoList },
    { path: "/dashboard", component: Dashboard }
];

export default createRouter({
    history: createWebHistory(),
    routes
});
