import axios from 'axios';
import {ref} from 'vue';

export async function getGameUserStatus(gameId, userId) {
    const status = ref()
    const isListLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/Game/${gameId}/Status/${userId}`);
            
        status.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        status
    }
}