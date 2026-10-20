import { createApp } from 'vue'
import App from './App.vue'
import { router } from './router.js' // <-- Добавили импорт

const app = createApp(App)
app.use(router) // <-- Подключили роутер
app.mount('#app')
