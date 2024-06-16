<template>
    <div>
      <h1>Мой список сериалов</h1>
  
      <div v-if="loading" class="loading">Идет загрузка...</div>
      <div v-if="error" class="error">{{ error }}</div>
      <div v-if="tvShowList" class="content">
  
        <table class="table">
          <tr>
            <th>
              Название сериала
            </th>
            <th>
              Статус
            </th>
            <th>
              Просмотрено серий
            </th>
            <th>
              Оценка
            </th>
          </tr>
          <tr v-for="item in tvShowList">
            <td>
              <p class="linked" @click="goToTvShowPage(item.tvShow_id)">{{ item.title_ru }}</p>
            </td>
            <td>
              {{ item.status }}
            </td>
            <td>
              <span class="linked" @click="changeEpisodes(item.tvShow_id, item.episodes_watched - 1)">-</span>
              {{ item.episodes_watched }}
              <span class="linked" @click="changeEpisodes(item.tvShow_id, item.episodes_watched + 1)">+</span>
            </td>
            <td>
              
                <input class="score" v-model="item.user_rating" type="number" @input="changeRating(item.tvShow_id, item.user_rating)">
  
            </td>
          </tr>
        </table>
  
      </div>
    </div>
  </template>
  
  <script>
  import { ref, watch } from 'vue'
  import { getTvShowList } from '@/hooks/getList';
  import { changeTvShowUserStatus } from '@/hooks/changeUserStatus';
  import { changeTvShowUserRating } from '@/hooks/changeListData';
  import { changeTvShowWatchedEp } from '@/hooks/changeListData';
  import { useRoute } from "vue-router";
  
  export default {
    components: {
      
    },
    data() {
      return {
        loading: false,
        tvShowList: [],        
        error: null
      }
    },
    created() {
      // watch the params of the route to fetch the data again
      this.$watch(
        () => this.$route.params.tvShowId,
        this.fetchTvShowListData,
        // fetch the data when the view is created and the data is
        // already being observed
        { immediate: true }
      )
    },
    methods: {
      goToTvShowPage(id) {
        this.$router.push(`/TvShow/${id}/`);
      },
      async changeRating(tvShowId, newRating) {
        if (newRating > 0 && newRating <= 10) {
          try {
          await changeTvShowUserRating(tvShowId, 1, newRating);
          await this.fetchTvShowListData(1);
        } catch (err) {
          this.error = err.toString()
        } finally {
  
        }
        }
        
      },
      async changeEpisodes(tvShowId, newEps) {
        try {
          await changeTvShowWatchedEp(tvShowId, 1, newEps);
          await this.fetchTvShowListData(1);
        } catch (err) {
          this.error = err.toString()
        } finally {
  
        }
      },
      async fetchTvShowListData(id) {
        this.error = this.tvShowList = null
        this.loading = true
  
        try {
          // replace `getPost` with your data fetching util / API wrapper
          this.tvShowList = (await getTvShowList(id)).tvShowList;
  
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