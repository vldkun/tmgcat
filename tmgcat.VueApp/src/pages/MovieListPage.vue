<template>
    <div>
      <h1>Мой список фильмов</h1>
  
      <div v-if="loading" class="loading">Идет загрузка...</div>
      <div v-if="error" class="error">{{ error }}</div>
      <div v-if="movieList" class="content">
  
        <table class="table">
          <tr>
            <th>
              Название фильма
            </th>
            <th>
              Статус
            </th>
            <th>
              Оценка
            </th>
          </tr>
          <tr v-for="item in movieList">
            <td>
              <p class="linked" @click="goToMoviePage(item.movie_id)">{{ item.title_ru }}</p>
            </td>
            <td>
              {{ item.status }}
            </td>
            <td>
              
                <input class="score" v-model="item.user_rating" type="number" @input="changeRating(item.movie_id, item.user_rating)">
  
            </td>
          </tr>
        </table>
  
      </div>
    </div>
  </template>
  
  <script>
  import { ref, watch } from 'vue'
  import { getMovieList } from '@/hooks/getList';
  import { changeMovieUserStatus } from '@/hooks/changeUserStatus';
  import { changeMovieUserRating } from '@/hooks/changeListData';
  import { useRoute } from "vue-router";
  import Grid from '@/components/Grid.vue';
  
  export default {
    components: {
      Grid
    },
    data() {
      return {
        loading: false,
        movieList: [],
        
        error: null,
      }
    },
    created() {
      // watch the params of the route to fetch the data again
      this.$watch(
        () => this.$route.params.userId,
        this.fetchMovieListData,
        // fetch the data when the view is created and the data is
        // already being observed
        { immediate: true }
      )
    },
    methods: {
      goToMoviePage(id) {
        this.$router.push(`/Movie/${id}/`);
      },
      async changeRating(movieId, newRating) {
        if (newRating > 0 && newRating <= 10) {
          try {
          await changeMovieUserRating(movieId, 1, newRating);
          await this.fetchMovieListData(1);
        } catch (err) {
          this.error = err.toString()
        } finally {
  
        }
        }
        
      },
    
      async fetchMovieListData(id) {
        this.error = this.movieList = null
        this.loading = true
  
        try {
          // replace `getPost` with your data fetching util / API wrapper
          this.movieList = (await getMovieList(id)).movieList;
  
        } catch (err) {
          this.error = err.toString()
        } finally {
          this.loading = false
        }
      },
    },
  }
  
  </script>
  
  <style scoped>
  .score {
    background: none;
    border: none;
    color: rgb(32, 32, 32);
    font-family: inherit;
      font-size: inherit;
    padding: 1px 82px 0 88px;
    text-overflow: ellipsis;
    width: 100%;
  }
  
  .linked {
    cursor: pointer;
  }
  
  .linked:hover {
    font-weight: bold;
  }
  
  .table {
    width: 100%;
    margin-bottom: 20px;
    border: 1px solid #dddddd;
    border-collapse: collapse;
  }
  
  .table th {
    font-weight: bold;
    padding: 5px;
    background: #efefef;
    border: 1px solid #dddddd;
  }
  
  .table td {
    border: 1px solid #b7b7b7;
    padding: 5px;
    background-color: #dddddd;
  }
  </style>