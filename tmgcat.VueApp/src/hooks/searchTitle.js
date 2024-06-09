import axios from 'axios';
import {ref} from 'vue';

export async function searchGames(query) {
    const titles = ref([])    
    try {
        const response = await axios.get(encodeURI(`https://localhost:7112/Search/Games?query=${query}`));
        titles.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }
    
    return {
        titles
    }
}