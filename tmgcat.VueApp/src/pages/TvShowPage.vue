<template>
    <div class="title-page">
        <div v-if="loading" class="title-page-loading">Идет загрузка...</div>
        <div v-if="error" class="title-page-error">{{ error }}</div>
        <div v-if="tvShow" class="title-page-content">
            <div class="title-page-head">
                <h1>
                    {{ tvShow.title_en }} / {{ tvShow.title_ru }}
                </h1>
            </div>
            <div class="title-page-data">
                <div class="title-page-image">
                    <img :src="getUrl(tvShow.poster_path)" width=225px />
                </div>
                <div class="title-page-info">
                    <h2>
                        Информация
                    </h2>
                    <table class="title-page-info-table">
                        <tr>
                            <td>Жанры:</td>
                            <td>{{ tvShow.genres }}</td>
                        </tr>
                        <tr>
                            <td>Количество сезонов:</td>
                            <td>{{ tvShow.seasons_number }}</td>
                        </tr>
                        <tr>
                            <td>Количество серий:</td>
                            <td>{{ tvShow.episodes_number }}</td>
                        </tr>
                        <tr>
                            <td>Длина серии:</td>
                            <td>{{ tvShow.avg_ep_runtime }} мин.</td>
                        </tr>
                        <tr>
                            <td>Создатели: </td>
                            <td>{{ tvShow.creators }}</td>
                        </tr>
                        <tr>
                            <td>Статус: </td>
                            <td>{{ tvShow.status }}</td>
                        </tr>
                        <tr>
                            <td>Дата начала показа:</td>
                            <td>{{ formatDate(tvShow.first_air_date) }}</td>
                        </tr>
                        <tr>
                            <td>Дата окончания показа:</td>
                            <td>{{ formatDate(tvShow.last_air_date) }}</td>
                        </tr>
                    </table>

                    <h2>
                        Оценки
                    </h2>
                                           
                    <p>Средняя оценка пользователей:
                    <span class="title-page-score">
                         {{ tvShow.user_rating }}
                    </span>
                    </p>
                    <p>Оценка IMDB:
                    <span class="title-page-score">
                         {{ tvShow.imdb_rating }}
                    </span>
                    </p>
                    <p>Оценка Кинопоиска:
                    <span class="title-page-score">
                         {{ tvShow.kp_rating }}
                    </span>
                    </p>
                    <h2>
                        Мой Статус
                    </h2>
                    <div class="title-page-app__btns">
                        <button class="title-page-button" :class="{
                            'title-page-button-selected': userStatus === 'Watching'
                        }" @click="changeStatus('Watching')">
                            Смотрю
                        </button>
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
                {{ tvShow.description }}
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
import { getTvShow } from '@/hooks/getTitle';
import { getTvShowUserStatus } from '@/hooks/getUserStatus';
import { changeTvShowUserStatus } from '@/hooks/changeUserStatus';
import { postTvShowComment } from '@/hooks/postComment';
import { getTvShowComments } from '@/hooks/getComments';
import formatDateMixin from '@/mixins/formatDateMixin';

export default {
    components() {

    },
    data() {
        return {
            replyCommentId: null,
            message: '',
            loading: false,
            tvShow: null,
            comments: [],
            error: null,
            firstAirDate: new Date(),
            lastAirDate: new Date(),
            userStatus: null
        }
    },
    created() {
        // watch the params of the route to fetch the data again
        this.$watch(
            () => this.$route.params.tvShowId,
            this.fetchTvShowData,
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
        goToTvShowPage(id) {
            this.$router.push(`/TvShow/${id}/`);
        },
        getUrl(link) {
            return new URL(link, import.meta.url).href;
        },
        async changeStatus(newStatus) {
            if (this.userStatus != newStatus) {
                try {
                    await changeTvShowUserStatus(this.$route.params.tvShowId, 1, newStatus);
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
                await postTvShowComment(this.$route.params.tvShowId, 1, this.message, this.replyCommentId);
            } catch (err) {
                this.error = err.toString()
            } finally {
                location.reload();
            }
        },
        async fetchTvShowData(id) {
            this.error = this.tvShow = null
            this.loading = true

            try {
                this.tvShow = (await getTvShow(id)).tvShow
                this.firstAirDate = this.tvShow.first_air_date
                this.lastAirDate = this.tvShow.last_air_date
                this.userStatus = (await getTvShowUserStatus(id, 1)).status;
                this.comments = (await getTvShowComments(id)).comments;
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
            return this.formatDate(this.firstAirDate);
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