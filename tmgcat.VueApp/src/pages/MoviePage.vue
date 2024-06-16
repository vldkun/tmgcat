<template>
    <div class="title-page">
        <div v-if="loading" class="title-page-loading">Идет загрузка...</div>
        <div v-if="error" class="title-page-error">{{ error }}</div>
        <div v-if="movie" class="title-page-content">
            <div class="title-page-head">
                <h1>
                    {{ movie.title_en }} / {{ movie.title_ru }}
                </h1>
            </div>
            <div class="title-page-data">
                <div class="title-page-image">
                    <img :src="getUrl(movie.poster_path)" width=225px />
                </div>
                <div class="title-page-info">
                    <h2>
                        Информация
                    </h2>
                    <table class="title-page-info-table">
                        <tr>
                            <td>Жанры:</td>
                            <td>{{ movie.genres }}</td>
                        </tr>
                        <tr>
                            <td>Продолжительность:</td>
                            <td>{{ movie.avg_ep_runtime }}</td>
                        </tr>
                        <tr>
                            <td>Создатели: </td>
                            <td>{{ movie.creators }}</td>
                        </tr>
                        <tr>
                            <td>Статус: </td>
                            <td>{{ movie.status }}</td>
                        </tr>
                        <tr>
                            <td>Дата выхода:</td>
                            <td>{{ formatDate(releaseDate) }}</td>
                        </tr>
                    </table>

                    <h2>
                        Оценки
                    </h2>
                                           
                    <p>Средняя оценка пользователей:
                    <span class="title-page-score">
                         {{ movie.user_rating }}
                    </span>
                    </p>
                    <p>Оценка IMDB:
                    <span class="title-page-score">
                         {{ movie.imdb_rating }}
                    </span>
                    </p>
                    <p>Оценка Кинопоиска:
                    <span class="title-page-score">
                         {{ movie.kp_rating }}
                    </span>
                    </p>
                    <h2>
                        Мой Статус
                    </h2>
                    <div class="title-page-app__btns">
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Watched'
                        }" @click="changeStatus('Watched')">
                            Просмотрено
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
                {{ movie.description }}
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
import { getMovie } from '@/hooks/getTitle';
import { getMovieUserStatus } from '@/hooks/getUserStatus';
import { changeMovieUserStatus } from '@/hooks/changeUserStatus';
import { postMovieComment } from '@/hooks/postComment';
import { getMovieComments } from '@/hooks/getComments';
import formatDateMixin from '@/mixins/formatDateMixin';

export default {
    components() {

    },
    data() {
        return {
            replyCommentId: null,
            message: '',
            loading: false,
            movie: null,
            comments: [],
            error: null,
            releaseDate: new Date(),
            userStatus: null
        }
    },
    created() {
        // watch the params of the route to fetch the data again
        this.$watch(
            () => this.$route.params.movieId,
            this.fetchMovieData,
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
        goToMoviePage(id) {
            this.$router.push(`/Movie/${id}/`);
        },
        getUrl(link) {
            return new URL(link, import.meta.url).href;
        },
        async changeStatus(newStatus) {
            if (this.userStatus != newStatus) {
                try {
                    await changeMovieUserStatus(this.$route.params.movie, 1, newStatus);
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
                await postMovieComment(this.$route.params.movieId, 1, this.message, this.replyCommentId);
            } catch (err) {
                this.error = err.toString()
            } finally {
                location.reload();
            }
        },
        async fetchMovieData(id) {
            this.error = this.movie = null
            this.loading = true

            try {
                this.movie = (await getMovie(id)).movie
                this.releaseDate = this.movie.released_at
                this.userStatus = (await getMovieUserStatus(id, 1)).status;
                this.comments = (await getMovieComments(id)).comments;
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