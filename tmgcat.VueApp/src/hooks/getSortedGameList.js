import {ref, computed} from 'vue'

export default function useSortedPosts(gameList) {
    const selectedSort = ref('')
    const sortedList = computed(() => {
        return [...gameList.value].sort((game1, game2) => game1[sortedList.value]?.localeCompare(game2[sortedList.value]))
    })

    return {
        selectedSort, sortedList
    }
};