<template>
    <div>
        <div v-if="loading" class="loading">Идет загрузка...</div>
        <div v-if="error" class="error">{{ error }}</div>
        <div v-if="game" class="content">
            <h1>
                {{ game.title }}
            </h1>
            <div>
                <div class="image">
                    <img :src="getUrl(game.cover_path)" />
                </div>
                <div class="info">
                    <table class="table">
                        <tr>
                            <td>Дата выхода:</td>
                            <td>{{ formatDate(releaseDate) }}</td>
                        </tr>
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
                            <td>Категория: </td>
                            <td>{{ game.category }}</td>
                        </tr>
                    </table>
                </div>
                <h2>
                    Описание
                </h2>
                <div class="description">
                    {{ game.description }}
                </div>
                <div class="app__btns">
                    <status-selector 
                        class="status"
                        :class = "{
                            'current-status': userStatus=== 'Playing'
                        }"
                        @click="changeStatus('Playing')"
                    >
                        Играю
                    </status-selector>
                    <status-selector 
                        class="status"
                        :class = "{
                            'current-status': userStatus=== 'Played'
                        }"
                        @click="changeStatus('Played')"
                    >
                        Играл
                    </status-selector>
                    <status-selector 
                        class="status"
                        :class = "{
                            'current-status': userStatus === 'Planned'
                        }"
                        @click="changeStatus('Planned')"
                    >
                        Запланировано
                    </status-selector>
                    <status-selector 
                        class="status"
                        :class = "{
                            'current-status': userStatus === 'Not planned'
                        }"
                        @click="changeStatus('Not planned')"
                    >
                        Не в планах
                    </status-selector>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { getGame } from '@/hooks/getGame';
import { getGameUserStatus } from '@/hooks/getGameUserStatus';
import { changeGameUserStatus } from '@/hooks/changeGameUserStatus';
import formatDateMixin from '@/mixins/formatDateMixin';
import StatusSelector from '@/components/StatusSelector.vue';

export default {
    components() {
        StatusSelector
    },
    data() {
        return {
            loading: false,
            game: null,
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
        async fetchGameData(id) {
            this.error = this.game = null
            this.loading = true

            try {
                // replace `getPost` with your data fetching util / API wrapper
                this.game = (await getGame(id)).game
                this.releaseDate = this.game.released_at
                this.userStatus = (await getGameUserStatus(id, 1)).status;
                console.log(this.userStatus);

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
.status {
    margin: 10px;
    height: 40px;
    width: 140px;
    border: #3e3e3e;
    border: 5px;    
    border-radius: 4px;   
    background-color: #dcdcdc;    
}
.current-status {
    margin: 10px;
    height: 40px;
    width: 140px;
    border: #3e3e3e;
    border: 5px;
    border-radius: 4px;  
    color: #dcdcdc; 
    background-color: #747474;    
}
.app__btns {    
    display: flex;
    justify-content: center;
    text-align: center;
    vertical-align: middle;
    align-items: center;
}

.image {
    height: auto;
    max-width: 100%;
    display: inline-block;
}

.description {
    height: 100px;
    width: 600px;
    display: inline-block;
}

.info {
    width: 300px;
    margin-top: auto;
    margin-left: 10px;
    display: inline-block;
}

.table {
    width: 100%;
    border: none;
    margin-bottom: 20px;
}

.table thead th {
    padding: 10px;
    font-weight: 500;
    font-size: 16px;
    line-height: 20px;
    text-align: left;
    color: #444441;
    border-top: 2px solid #7c7c7c;
    border-bottom: 2px solid #7c7c7c;
}

.table tbody td {
    padding: 10px;
    font-size: 14px;
    line-height: 20px;
    color: #444441;
    border-top: 1px solid #7c7c7c;
}
</style>