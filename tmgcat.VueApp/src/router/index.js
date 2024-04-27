import { createRouter, createWebHistory } from 'vue-router'
import GamePage from '@/pages/GamePage.vue'
import GameListPage from '@/pages/GameListPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/Game/:gameId',
      name: 'game',
      component: GamePage
    },
    {
      path: '/Users/:userId/GameList',
      name: 'gameList',
      component: GameListPage
    }

  ]
})

export default router
