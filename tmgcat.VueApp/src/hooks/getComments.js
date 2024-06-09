import axios from 'axios';
import {ref} from 'vue';

export async function getGameComments(gameId) {
    const comments = ref([])
    const isCommentsLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/Game/${gameId}/Comments`);
        comments.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isCommentsLoading.value = false;
    }
    
    return {
        comments
    }
}