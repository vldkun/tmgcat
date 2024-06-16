import axios from 'axios';
import {ref} from 'vue';

export async function changeGameUserTime(gameId, userId, time) {    
    try {
        await axios.post(`https://localhost:7112/Game/${gameId}/Played/${userId}?time=${time}`, {
            params: {
                time: time
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        
    }
}

export async function changeGameUserRating(gameId, userId, score) {    
    try {
        await axios.post(`https://localhost:7112/Game/${gameId}/Score/${userId}?score=${score}`, {
            params: {
                score: score
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        
    }
}

export async function changeTvShowUserRating(tvShowId, userId, score) {    
    try {
        await axios.post(`https://localhost:7112/TvShow/${tvShowId}/Score/${userId}?score=${score}`, {
            params: {
                score: score
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        
    }
}

export async function changeTvShowWatchedEp(tvShowId, userId, episodes) {    
    try {
        await axios.post(`https://localhost:7112/TvShow/${tvShowId}/Episodes/${userId}?episodes=${episodes}`, {
            params: {
                episodes: episodes
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        
    }
}

export async function changeMovieUserRating(movieId, userId, score) {    
    try {
        await axios.post(`https://localhost:7112/Movie/${movieId}/Score/${userId}?score=${score}`, {
            params: {
                score: score
            }
        });
        
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }    
    return {
        
    }
}