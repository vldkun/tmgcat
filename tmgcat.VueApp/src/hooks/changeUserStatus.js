import axios from 'axios';
import {ref} from 'vue';

export async function changeGameUserStatus(gameId, userId, status) {    
    try {
        await axios.post(`https://localhost:7112/Game/${gameId}/Status/${userId}?status=${status}`, {
            params: {
                status: status
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }
    
    return {
        
    }
}

export async function changeMovieUserStatus(movieId, userId, status) {    
    try {
        await axios.post(`https://localhost:7112/Movie/${movieId}/Status/${userId}?status=${status}`, {
            params: {
                status: status
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }
    
    return {
        
    }
}

export async function changeTvShowUserStatus(tvShowId, userId, status) {    
    try {
        await axios.post(`https://localhost:7112/TvShow/${tvShowId}/Status/${userId}?status=${status}`, {
            params: {
                status: status
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }
    
    return {
        
    }
}