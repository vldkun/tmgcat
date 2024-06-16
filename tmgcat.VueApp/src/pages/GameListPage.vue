<template>
  <div>
    <h1>Мой список игр</h1>

    <div v-if="loading" class="loading">Идет загрузка...</div>
    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="gameList" class="content">

      <table class="table">
        <tr>
          <th>
            Название игры
          </th>
          <th>
            Статус
          </th>
          <th>
            Наиграно
          </th>
          <th>
            Оценка
          </th>
        </tr>
        <tr v-for="item in gameList">
          <td>
            <p class="linked" @click="goToGamePage(item.game_id)">{{ item.title }}</p>
          </td>
          <td>
            {{ item.status }}
          </td>
          <td>
            <span class="linked" @click="changeTime(item.game_id, item.minutes_played - 1)">-</span>
            {{ item.minutes_played }}
            <span class="linked" @click="changeTime(item.game_id, item.minutes_played + 1)">+</span>
          </td>
          <td>
            
              <input class="score" v-model="item.user_rating" type="number" @input="changeRating(item.game_id, item.user_rating)">

          </td>
        </tr>
      </table>

    </div>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
import { getGameList } from '@/hooks/getList';
import GameList from '@/components/GameList.vue';
import { changeGameUserStatus } from '@/hooks/changeUserStatus';
import { changeGameUserRating } from '@/hooks/changeListData';
import { changeGameUserTime } from '@/hooks/changeListData';
import GameListItem from '@/components/GameListItem.vue';
import { useRoute } from "vue-router";
import getSortedGameList from "@/hooks/getSortedGameList";
import Grid from '@/components/Grid.vue';

export default {
  components: {
    GameList,
    Grid
  },
  data() {
    return {
      loading: false,
      gameList: [],
      //gridColumns: ['name', 'power', 'dfs'],
      gridData: [
        { title: 'sda', status: 'asda', minutes_played: 'asda', user_rating: 0 }
      ],
      error: null,
      searchQuery: '',
      gridColumns: ['Название игры', 'Статус', 'Наиграно', 'Оценка'],
    }
  },
  created() {
    // watch the params of the route to fetch the data again
    this.$watch(
      () => this.$route.params.userId,
      this.fetchGameListData,
      // fetch the data when the view is created and the data is
      // already being observed
      { immediate: true }
    )
  },
  methods: {
    goToGamePage(id) {
      this.$router.push(`/Game/${id}/`);
    },
    async changeRating(gameId, newRating) {
      if (newRating > 0 && newRating <= 10) {
        try {
        await changeGameUserRating(gameId, 1, newRating);
        await this.fetchGameListData(1);
      } catch (err) {
        this.error = err.toString()
      } finally {

      }
      }
      
    },
    async changeTime(gameId, newTime) {
      try {
        await changeGameUserTime(gameId, 1, newTime);
        await this.fetchGameListData(1);
      } catch (err) {
        this.error = err.toString()
      } finally {

      }
    },
    async fetchGameListData(id) {
      this.error = this.gameList = null
      this.loading = true

      try {
        // replace `getPost` with your data fetching util / API wrapper
        this.gameList = (await getGameList(id)).gameList;

      } catch (err) {
        this.error = err.toString()
      } finally {
        this.loading = false
      }
    },
  },
}


/*export default {
    
    data() {
        return {

        }
    },
    setup(props) {
        const route = useRoute()       
        //const { gameList, isListLoading } = getGameList(route.params.userId)
        //const {sortedList, selectedSort} = getSortedGameList(gameList);
        
        return //gameList, isListLoading, sortedList, selectedSort
    }  

}*/
</script>

<style scoped>
.score {
  background: none;
  border: none;
  color: rgb(32, 32, 32);
  font-family: inherit;
    font-size: inherit;
  padding: 1px 5px 0 5px;
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