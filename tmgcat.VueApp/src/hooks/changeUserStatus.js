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