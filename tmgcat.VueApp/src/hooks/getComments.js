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

export async function getMovieComments(movieId) {
    const comments = ref([])
    const isCommentsLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/Movie/${movieId}/Comments`);
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

export async function getTvShowComments(tvShowId) {
    const comments = ref([])
    const isCommentsLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/TvShow/${tvShowId}/Comments`);
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

export async function getUserComments(userId) {
    const comments = ref([])
    const isCommentsLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/User/${userId}/Comments`);
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