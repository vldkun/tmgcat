<template>
    <div v-if= gameList.length > 0">
        <h3>Список игр</h3>
        <transition-group name="game-list">
            <game-list-item
                v-for="game in gameList"
                :title="game.title"
                :key="game.game_id"
            />
        </transition-group>
    </div>
    <h2 v-else style="color: red">
        Список игр пуст
    </h2>
</template>
  
<script>
import GameListItem from "@/components/GameListItem.vue";
export default {
    components: { GameListItem },
    props: {
        gameList: {
            type: Array,
            required: true
        }
    }
}
</script>
  
<style scoped>
  .game-list-item {
    display: inline-block;
    margin-right: 10px;
  }
  .game-list-enter-active,
  .game-list-leave-active {
    transition: all 0.4s ease;
  }
  .game-list-enter-from,
  .game-list-leave-to {
    opacity: 0;
    transform: translateX(130px);
  }
  .game-list-move {
    transition: transform 0.4s ease;
  }
</style>