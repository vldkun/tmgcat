import { createRouter, createWebHistory } from 'vue-router'
import GamePage from '@/pages/GamePage.vue'
import UserPage from '@/pages/UserPage.vue'
import GameListPage from '@/pages/GameListPage.vue'
import TvShowPage from '@/pages/TvShowPage.vue'
import TvShowListPage from '@/pages/TvShowListPage.vue'
import MoviePage from '@/pages/MoviePage.vue'
import MovieListPage from '@/pages/MovieListPage.vue'
import RegisterPage from '@/pages/RegisterPage.vue'
import LoginPage from '@/pages/LoginPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/Game/:gameId',
      name: 'game',
      component: GamePage
    },
    {
      path: '/User/:userId/GameList',
      name: 'gameList',
      component: GameListPage
    },
    {
      path: '/Movie/:movieId',
      name: 'movie',
      component: MoviePage
    },
    {
      path: '/User/:userId/MovieList',
      name: 'movieList',
      component: MovieListPage
    },
    {
      path: '/TvShow/:tvShowId',
      name: 'tvShow',
      component: TvShowPage
    },
    {
      path: '/User/:userId/TvShowList',
      name: 'tvShowList',
      component: TvShowListPage
    },
    {
      path: '/User/:userId/',
      name: 'userPage',
      component: UserPage
    },
    {
      path: '/Login/',
      name: 'loginPage',
      component: LoginPage
    },
    {
      path: '/Register/',
      name: 'registerPage',
      component: RegisterPage
    }

  ]
})

export default router
