import axios from 'axios';
import { ref } from 'vue';

export async function postGameComment(gameId, createdByUserId, content, parentCommentId = null) {
    const uri1 = `https://localhost:7112/Game/${gameId}/Comments?content=${content}&createdByUserId=${createdByUserId}&parentCommentId=${parentCommentId}`;
    const uri2 = `https://localhost:7112/Game/${gameId}/Comments?content=${content}&createdByUserId=${createdByUserId}`;
    try {
        if (parentCommentId != null) {
            await axios.post(encodeURI(uri1));
        } else {
            await axios.post(encodeURI(uri2));
        }
    } catch (e) {
        alert(e)
    } finally {
    }

    return {

    }
}