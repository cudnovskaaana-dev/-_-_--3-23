import { mockFetch } from './mockTickets.js'

const baseUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'
const useMock = import.meta.env.VITE_USE_MOCK !== 'false'

async function request(path, options) {
  const url = useMock ? `${baseUrl}/__mock${path}` : `${baseUrl}${path}`
  return (useMock ? mockFetch : fetch)(url, options)
}

export async function getTickets() {
  return request('/api/cable-tickets')
}

export async function createTicket(payload) {
  return request('/api/cable-tickets', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  })
}
