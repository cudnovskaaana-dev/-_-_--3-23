import { ref } from 'vue'

export const items = ref([
  { id: 1, number: 'КБ-101', title: 'Обрыв кабеля на участке Астана-Центр', siteId: 3, status: 'New' },
  { id: 2, number: 'КБ-102', title: 'Повреждение магистрали после грозы', siteId: 1, status: 'InProgress' }
])
