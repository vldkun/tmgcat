import axios from 'axios';
import {ref} from 'vue';

export async function getGameList(userId) {
    const gameList = ref([])
    const isListLoading = ref(true)
    
        try {
            console.log('Запрос к серверу до');
            const response = await axios.get('https://localhost:7112/GameList', {
                params: {
                    userId: userId
                }
            });
            console.log(response.data);
            console.log(response.data[0].title);
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