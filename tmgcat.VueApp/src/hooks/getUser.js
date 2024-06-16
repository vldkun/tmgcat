import axios from 'axios';
import {ref} from 'vue';

export async function getUser(userId) {
    const user = ref()
    const isListLoading = ref(true)
    
    try {
        const response = await axios.get(`https://localhost:7112/User/${userId}`);
            
        user.value = response.data;
    } catch (e) {
        alert('Ошибка')
    } finally {
        
    }
    
    return {
        user
    }
}