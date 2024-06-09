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
              {{ item.title }}
            </td>
            <td>
              {{ item.status }}
            </td>
            <td>
              {{ item.minutes_played }}
            </td>
            <td>
              {{ item.user_rating }}
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
import GameListItem from '@/components/GameListItem.vue';
import { useRoute } from "vue-router";
import getSortedGameList from "@/hooks/getSortedGameList";

export default {
  components: {
    GameList
  },
  data() {
    return {
      loading: false,
      gameList: [],
      error: null,
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
    async fetchGameListData(id) {
      this.error = this.gameList = null
      this.loading = true

      try {
        // replace `getPost` with your data fetching util / API wrapper
        this.gameList = (await getGameList(id)).gameList

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
	border: 1px solid #dddddd;
	padding: 5px;
}
</style>