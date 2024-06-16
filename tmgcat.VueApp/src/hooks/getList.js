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

export async function getTvShowList(id) {
    const tvShowList = ref([])
    const isListLoading = ref(true)
    
    try {
        console.log(id);
        const response = await axios.get('https://localhost:7112/TvShowList', {
            params: {
                userId: id
            }
        });
        console.log(response.data);
        tvShowList.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        tvShowList
    }
}

export async function getMovieList(id) {
    const movieList = ref([])
    const isListLoading = ref(true)
    
    try {
        console.log(id);
        const response = await axios.get('https://localhost:7112/MovieList', {
            params: {
                userId: id
            }
        });
        console.log(response.data);
        movieList.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        isListLoading.value = false;
    }
    
    return {
        movieList
    }
}