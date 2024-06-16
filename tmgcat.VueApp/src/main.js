import './assets/main.css'
import './assets/titlePage.css'
import './assets/comment.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from "primevue/config";
import Aura from '@primevue/themes/aura';

const app = createApp(App)

app.use(router)
app.use(PrimeVue, {
    unstyled: true
});

app.mount('#app')
