import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory} from "vue-router";

import App from './App.vue'
import Welcome from "../src/components/Welcome.vue";
import Login from './Login.vue';
import WeatherForecast from "../src/WeatherForcast.vue";
import Logout from "./Logout.vue";

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Welcome,
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
    },
    {
        path: '/weather',
        name: 'Weather',
        component: WeatherForecast,
    },
    {
        path: '/logout',
        name: 'Logout',
        component: Logout,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
})

createApp(App).use(router).mount('#app')
