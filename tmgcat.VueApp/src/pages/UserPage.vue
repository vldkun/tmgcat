<template>
    <div class="title-page">
        <div v-if="loading" class="title-page-loading">Идет загрузка...</div>
        <div v-if="error" class="title-page-error">{{ error }}</div>
        <div v-if="user" class="title-page-content">
            <div class="title-page-head">
                <h1>
                    {{ user.name }}
                </h1>
                <!--<span class="title-page-head-separator inline">/</span>-->
            </div>
            <div class="title-page-data">
                <div class="title-page-image">
                    <img :src="getUrl(user.profile_picture_path)" width=225px />
                </div>
                <div class="title-page-info">
                    
                    <table class="title-page-info-table">
                        
                        <tr>
                            <td>Дата регистрации:</td>
                            <td>{{ formatDate(registrationDate) }}</td>
                        </tr>
                        
                    </table>

                    <h2>
                        Списки пользователя
                    </h2>
                    <div class="title-page-app__btns">
                        <button class="title-page-button"  @click="goToMovieList(user.user_id)">
                            Фильмы
                        </button>
                        <button class="title-page-button"  @click="goToTvShowList(user.user_id)">
                            Сериалы
                        </button>
                        <button class="title-page-button"  @click="goToGameList(user.user_id)">
                            Игры
                        </button>
                        
                    </div>
                </div>
            </div>
            <h2>
                О пользователе
            </h2>
            <div class="title-page-description">
                {{ user.info }}
            </div>
            <h2>
                История
            </h2>
            <div  class="title-page-description">
                <p v-if="user.user_id==1">Начал играть в Fallout 3</p>
            </div>
            <h2>
                Комментарии
            </h2>
            <div class="comment-block">
                <div v-if="comments.length == 0">Комментариев пока нет.</div>
                <div v-if="comments.length > 0" class="comment-all">
                    <div v-for="comment in comments">
                        <div class="comment-single" :style="`margin-left: ${(comment.level-1) * 5}%`">
                            <div class="comment-content">
                                <header class="comment-header">
                                    <div class="comment-user-pic">
                                        <img :src="getUrl(comment.user_profile_picture_path)" width=48px
                                            @click="goToProfile(comment.created_by_user_id)" />
                                    </div>
                                    <div class="comment-name-date">
                                        <div class="comment-user-name" @click="goToProfile(comment.created_by_user_id)">
                                            {{ comment.user_name }}
                                        </div>
                                        <span class="comment-date">
                                            Отправлено {{ formatDateFull(comment.created_at) }}
                                        </span>
                                    </div>
                                </header>
                                <div class="comment-body">
                                    {{ comment.content }}
                                </div>
                                <div @click="addReply(comment.id)" class="comment-reply">
                                    Ответить
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <h2>
                    Комментировать
                </h2>
                <div class="comment-editor">
                    <textarea v-model="message" class="comment-input" placeholder="Текст комментария"></textarea>
                    <div class="title-page-app__btns">
                        <button class="title-page-button" @click="newComment()">Отправить</button>
                        <button v-if="replyCommentId != null" class="title-page-button" @click="removeReply()">Не отвечать</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { getUser } from '@/hooks/getUser';
import { postUserComment } from '@/hooks/postComment';
import { getUserComments } from '@/hooks/getComments';
import formatDateMixin from '@/mixins/formatDateMixin';

export default {
    components() {

    },
    data() {
        return {
            replyCommentId: null,
            message: '',
            loading: false,
            user: null,
            comments: [],
            error: null,
            registrationDate: new Date()
        }
    },
    created() {
        // watch the params of the route to fetch the data again
        this.$watch(
            () => this.$route.params.userId,
            this.fetchUserData,
            // fetch the data when the view is created and the data is
            // already being observed
            { immediate: true }
        )
    },
    methods: {
        addReply(id) {
            this.replyCommentId = id;
        },
        removeReply() {
            this.replyCommentId = null;
        },
        goToProfile(id) {
            this.$router.push(`/User/${id}/`);
        },
        goToGamePage(id) {
            this.$router.push(`/Game/${id}/`);
        },
        goToMovieList(id) {
      this.$router.push(`/User/${id}/MovieList`);
    },
    goToTvShowList(id) {
      this.$router.push(`/User/${id}/TvShowList`);
    },
    goToGameList(id) {
      this.$router.push(`/User/${id}/GameList`);
    },
        getUrl(link) {
            return new URL(link, import.meta.url).href;
        },
        
        async newComment() {
            try {
                console.log(this.replyCommentId);
                await postUserComment(this.$route.params.userId, 1, this.message, this.replyCommentId);
            } catch (err) {
                this.error = err.toString()
            } finally {
                location.reload();
            }
        },
        async fetchUserData(id) {
            this.error = this.user = null
            this.loading = true

            try {
                this.user = (await getUser(id)).user
                this.registrationDate = this.user.created_at
                this.comments = (await getUserComments(id)).comments;
            } catch (err) {
                this.error = err.toString()
            } finally {
                this.loading = false
            }
        },
    },
    mixins: [formatDateMixin],
    computed: {
        formattedDate() {
            return this.formatDate(this.registrationDate);
        }
    }
}
</script>
<style scoped>
table {
    table-layout: fixed;
    width: 100%;
    border-collapse: collapse;
    border: none;
}

table td:nth-child(1) {
    width: 10%;
}

th,
td {
    padding: 2px;
    width: 30%;
}
</style>