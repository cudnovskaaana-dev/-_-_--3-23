import { createRouter, createWebHistory } from 'vue-router'
import TicketList from './pages/TicketList.vue'
import TicketNew from './pages/TicketNew.vue'
import Login from './pages/Login.vue'

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: TicketList },
    { path: '/new', component: TicketNew },
    { path: '/login', component: Login }
  ]
})
