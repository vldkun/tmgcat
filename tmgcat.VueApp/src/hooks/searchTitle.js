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

export async function searchMovies(query) {
    const titles = ref([])    
    try {
        const response = await axios.get(encodeURI(`https://localhost:7112/Search/Movies?query=${query}`));
        titles.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        titles
    }
}

export async function searchTvShows(query) {
    const titles = ref([])    
    try {
        const response = await axios.get(encodeURI(`https://localhost:7112/Search/TvShows?query=${query}`));
        titles.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        titles
    }
}