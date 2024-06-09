import './assets/main.css'
import './assets/titlePage.css'
import './assets/comment.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(router)

app.mount('#app')
