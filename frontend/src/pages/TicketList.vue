<script setup>
import { onMounted, ref } from 'vue'
import TicketTable from '../components/TicketTable.vue'
import { items } from '../ticketsStore.js'
import { getTickets } from '../api/tickets.js'

const loading = ref(false)
const error = ref('')
async function loadTickets() {
  loading.value = true
  error.value = ''
  try {
    const response = await getTickets()
    if (!response.ok) {
      error.value = response.status >= 500 ? 'Сервер сломался. Попробуйте позже.' : 'Не удалось загрузить заявки.'
      return
    }
    items.value = await response.json()
  } catch {
    error.value = 'Нет связи. Проверьте адрес API и доступность сервера.'
  } finally {
    loading.value = false
  }
}
onMounted(loadTickets)
</script>

<template>
  <main>
    <h2>Заявки на повреждение кабеля</h2>
    <p v-if="loading">Загружаю заявки…</p>
    <p v-else-if="error" role="alert">{{ error }} <button @click="loadTickets">Повторить</button></p>
    <p v-else-if="items.length === 0">Заявок пока нет.</p>
    <TicketTable v-else :items="items" />
  </main>
</template>
