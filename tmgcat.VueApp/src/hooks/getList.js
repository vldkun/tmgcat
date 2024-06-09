import axios from 'axios';
import {ref} from 'vue';

export async function getGameList(id) {
    const gameList = ref([])
    const isListLoading = ref(true)
    
    try {
        console.log(id);
        const response = await axios.get('https://localhost:7112/GameList', {
            params: {
                userId: id
            }
        });
        console.log(response.data);
        gameList.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        gameList
    }
}