<script setup>
defineProps({
  items: { type: Array, required: true }
})
</script>

<template>
  <table class="tickets">
    <thead>
      <tr>
        <th>Номер</th>
        <th>Участок</th>
        <th>Тема</th>
        <th>Статус</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.number }}</td>
        <td>{{ item.siteId }}</td>
        <td>{{ item.title }}</td>
        <td>{{ item.status }}</td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped>
.tickets { width: 100%; border-collapse: collapse; }
.tickets th, .tickets td { border: 1px solid #ccc; padding: 8px; text-align: left; }
.tickets th { background: #f5f5f5; }
</style>
