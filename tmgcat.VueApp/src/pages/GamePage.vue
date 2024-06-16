<template>
    <div class="title-page">
        <div v-if="loading" class="title-page-loading">Идет загрузка...</div>
        <div v-if="error" class="title-page-error">{{ error }}</div>
        <div v-if="game" class="title-page-content">
            <div class="title-page-head">
                <h1>
                    {{ game.title }}
                </h1>
                <!--<span class="title-page-head-separator inline">/</span>-->
            </div>
            <div class="title-page-data">
                <div class="title-page-image">
                    <img :src="getUrl(game.cover_path)" width=225px />
                </div>
                <div class="title-page-info">
                    <h2>
                        Информация
                    </h2>
                    <table class="title-page-info-table">
                        <tr>
                            <td>Жанры:</td>
                            <td>{{ game.genres }}</td>
                        </tr>
                        <tr>
                            <td>Платформы:</td>
                            <td>{{ game.platforms }}</td>
                        </tr>
                        <tr>
                            <td>Разработчики: </td>
                            <td>{{ game.involved_companies }}</td>
                        </tr>
                        <tr>
                            <td>Статус: </td>
                            <td>{{ game.status }}</td>
                        </tr>
                        <tr>
                            <td>Дата выхода:</td>
                            <td>{{ formatDate(releaseDate) }}</td>
                        </tr>
                        <tr>
                            <td>Категория: </td>
                            <td>{{ game.category }}</td>
                        </tr>
                    </table>

                    <h2>
                        Оценки
                    </h2>
                                           
                    <p>Средняя оценка пользователей:
                    <span class="title-page-score">
                         {{ game.user_rating }}
                    </span>
                    </p>
                    <h2>
                        Мой Статус
                    </h2>
                    <div class="title-page-app__btns">
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Playing'
                        }" @click="changeStatus('Playing')">
                            Играю
                        </button>
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Played'
                        }" @click="changeStatus('Played')">
                            Играл
                        </button>
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Planned'
                        }" @click="changeStatus('Planned')">
                            Запланировано
                        </button>
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Not planned'
                        }" @click="changeStatus('Not planned')">
                            Не в планах
                        </button>
                    </div>
                </div>
            </div>
            <h2>
                Описание
            </h2>
            <div class="title-page-description">
                {{ game.description }}
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
import { getGame } from '@/hooks/getTitle';
import { getGameUserStatus } from '@/hooks/getUserStatus';
import { changeGameUserStatus } from '@/hooks/changeUserStatus';
import { postGameComment } from '@/hooks/postComment';
import { getGameComments } from '@/hooks/getComments';
import formatDateMixin from '@/mixins/formatDateMixin';

export default {
    components() {

    },
    data() {
        return {
            replyCommentId: null,
            message: '',
            loading: false,
            game: null,
            comments: [],
            error: null,
            releaseDate: new Date(),
            userStatus: null
        }
    },
    created() {
        // watch the params of the route to fetch the data again
        this.$watch(
            () => this.$route.params.gameId,
            this.fetchGameData,
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
        getUrl(link) {
            return new URL(link, import.meta.url).href;
        },
        async changeStatus(newStatus) {
            if (this.userStatus != newStatus) {
                try {
                    await changeGameUserStatus(this.$route.params.gameId, 1, newStatus);
                    this.userStatus = newStatus;
                } catch (err) {
                    this.error = err.toString()
                } finally {

                }
            }
        },
        async newComment() {
            try {
                console.log(this.replyCommentId);
                await postGameComment(this.$route.params.gameId, 1, this.message, this.replyCommentId);
            } catch (err) {
                this.error = err.toString()
            } finally {
                location.reload();
            }
        },
        async fetchGameData(id) {
            this.error = this.game = null
            this.loading = true

            try {
                this.game = (await getGame(id)).game
                this.releaseDate = this.game.released_at
                this.userStatus = (await getGameUserStatus(id, 1)).status;
                this.comments = (await getGameComments(id)).comments;
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
            return this.formatDate(this.releaseDate);
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