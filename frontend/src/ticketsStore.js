import { ref } from 'vue'

// Начальные данные (из ЛР4)
export const items = ref([
  { id: 1, number: 'КБ-101', title: 'Обрыв кабеля на участке Астана-Центр', siteId: 3, status: 'New' },
  { id: 2, number: 'КБ-102', title: 'Повреждение магистрали после грозы', siteId: 1, status: 'InProgress' }
])

// Счётчик для новых ID (начинаем с 3)
let nextId = 3

// Функция добавления новой заявки
export function addItem(title, siteId, description) {
  items.value.push({
    id: nextId,
    number: `КБ-${100 + nextId}`, // Формируем красивый номер
    title: title,
    siteId: siteId,
    description: description,
    status: 'New' // Статус всегда New при создании!
  })
  nextId++ // Увеличиваем счетчик для следующей заявки
}
