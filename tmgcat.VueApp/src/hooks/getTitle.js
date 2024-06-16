import axios from 'axios';
import {ref} from 'vue';

export async function getGame(gameId) {
    const game = ref()
    const isListLoading = ref(true)
    
    try {
        const response = await axios.get('https://localhost:7112/Game', {
            params: {
                id: gameId
            }
        });
            
        game.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        game
    }
}

export async function getTvShow(tvShowId) {
    const tvShow = ref()
    const isListLoading = ref(true)
    
    try {
        const response = await axios.get('https://localhost:7112/TvShow', {
            params: {
                id: tvShowId
            }
        });
            
        tvShow.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        tvShow
    }
}

export async function getMovie(movieId) {
    const movie = ref()
    const isListLoading = ref(true)
    
    try {
        const response = await axios.get('https://localhost:7112/Movie', {
            params: {
                id: movieId
            }
        });
            
        movie.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        movie
    }
}