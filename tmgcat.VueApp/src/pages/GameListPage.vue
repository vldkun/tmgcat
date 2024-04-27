<template>
    <div>
        HJGJHGUKY
    </div>
    <div>
        <h1>Список игр</h1>
        <div v-if="loading" class="loading">Идет загрузка...</div>
        <div v-if="error" class="error">{{ error }}</div>
        <div v-if="gameList" class="content">
            <game-list
                :gameList="gameList"
            />     
        </div>
    </div>    
</template>

<script>
import { ref, watch } from 'vue'
import { getGameList } from '@/hooks/getGameList';
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
      this.fetchData,
      // fetch the data when the view is created and the data is
      // already being observed
      { immediate: true }
    )
  },
  methods: {
    async fetchData(id) {
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

</style>