<template>
  <header class="header">
    <div class="list-menu" @click="goToMovieList(userId)">
        Мои фильмы
    </div>
    <div class="list-menu" @click="goToTvShowList(userId)">
        Мои сериалы
    </div>
    <div class="list-menu" @click="goToGameList(userId)">
        Мои игры
    </div>
    <div class="profile-menu" @click="goToProfile(userId)">
      <div class="profile-pic">
        <img :src="getUrl(userProfilePic)" width=40px />
      </div>
      <div class="profile-name">
        {{ userName }}
      </div>
    </div>
    <div class="global-search">
      <label class="field">
        <select v-model="searchType" class="search-select" @change="ClearSearch()">
          <option>Игры</option>
          <option>Фильмы</option>
          <option>Сериалы</option>
        </select>
        <input placeholder="Поиск" @input="Search()" v-model="searchQuery">
        <img src="../src/assets/search-icon.svg" class="search-marker">

      </label>

      <div class="search-results" cli>
        <div v-for="item in searchResults" class="search-item">
          <div class="search-content" @click="goToTitle(item.id)">
            <header class="search-header">
              <div class="search-pic">
                <img :src="getUrl(item.poster_path)" width=48px />
              </div>
              <div class="search-title-date">
                <div class="search-title">
                  {{ item.title }}
                </div>
                <span v-if="searchType=='Игры'" class="search-date">
                  Дата выхода: {{ formatDateYear(item.released_at) }}
                </span>
                <span v-if="searchType=='Сериалы'" class="search-date">
                  Даты эфира: {{ formatDateYear(item.first_air_date) }} - {{ formatDateYear(item.last_air_date) }}
                </span>
                <span v-if="searchType=='Фильмы'" class="search-date">
                  Дата выхода: {{ formatDateYear(item.released_at) }}
                </span>
              </div>
            </header>
          </div>
        </div>
      </div>
    </div>
  </header>
  <div class="app">
    <router-view></router-view>
  </div>
</template>

<script>
import { searchGames } from '@/hooks/searchTitle';
import { getUser } from '@/hooks/getUser';
import formatDateMixin from '@/mixins/formatDateMixin';


export default {
  data() {
    return {
      searchResults: [],
      searchType: 'Игры',
      loading: false,
      searchQuery: '',
      user: null,
      userId: 1,
      userProfilePic: 'https://sun9-48.userapi.com/impg/TSvSYqrvAMQ1unoASYXuMcnFwUFiPZTh1aTRYw/VQDgRWolW34.jpg?size=467x467&quality=95&sign=3e8ab9686d8945c1e7bed31df5d174d0&type=album',
      userName: 'Vlad'
    }
  },
  created() {
    this.fetchUserData;
  },
  methods: {
    ClearSearch() {
      this.searchResults = [];
      this.searchQuery = ''
    },
    getUrl(link) {
      return new URL(link, import.meta.url).href;
    },
    goToProfile(id) {
      this.$router.push(`/User/${id}/`);
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
    goToTitle(id) {
      this.ClearSearch();
      switch (this.searchType) {
        case 'Игры':
          this.$router.push(`/Game/${id}/`);
          break;
        case 'Сериалы':
          this.$router.push(`/TvShow/${id}/`);
          break;
        case 'Фильмы':
          this.$router.push(`/Movie/${id}/`);
          break;
      }

    },
    async fetchUserData(id) {
      this.error = this.game = null
      this.loading = true

      try {
        this.user = (await getUser(id)).user
        this.userName = this.user.name
        this.userProfilePic = this.user.profile_picture_path;
      } catch (err) {
        this.error = err.toString()
      } finally {
        this.loading = false
      }
    },
    async Search() {
      console.log()
      if (this.searchQuery.length > 0) {
        try {
          switch (this.searchType) {
            case 'Игры':
              this.searchResults = (await searchGames(this.searchQuery)).titles;
              break;
            case 'Сериалы':
              this.searchResults = (await searchTvShows(this.searchQuery)).titles;
              break;
            case 'Фильмы':
              this.searchResults = (await searchMovies(this.searchQuery)).titles;
              break;
          }
        } catch (err) {
          this.error = err.toString()
        } finally {

        }
      } else {
        this.ClearSearch();
      }
    }
  },
  mixins: [formatDateMixin]
}
</script>

<style>
* {
  margin: 0 0;
  padding: 0;
  box-sizing: border-box;
}

.app {
  padding: 20px;
  display: block;
  max-width: 1280px;
  margin: 0 auto;
  background-color: #aaaaaa;
  min-height: 900px;
}

.header {
  padding-left: 100px;
  background: #343434;
  color: #1c1c1c;
  height: 50px;
  position: relative;
  z-index: 10;
  align-items: center;
  display: flex;
  box-shadow: 0 1px 5px #000000;
}

.title-page {
  padding: auto;
}

.profile-menu {
  position: absolute;
  right: 0;
  width: 150px;
  height: 50px;
  cursor: pointer;
}

.list-menu {
  position: relative;
  left: 0;
  width: 350px;
  height: 50px;
  cursor: pointer;
  font-size: 18px;
  color: #ffffff;
  padding-top: 10px;
  text-align: center;
  
}
.list-menu:hover {
  background-color: #4e4e4e;
}

.profile-menu:hover {
  background-color: #4e4e4e;
}

.profile-pic {
  position: absolute;
  left: 5px;
  top: 5px;
}

.profile-name {
  position: absolute;
  font-size: 18px;
  color: #fff;
  height: 50px;
  margin-top: 10px;
  margin-left: 55px;
}

.global-search {
  margin-left: 100px;
  margin-right: 400px;
  position: relative;
  width: 100%;
}

.global-search input {
  background: #fff;
  border-radius: 2px;
  border: 1px solid transparent;
  color: rgb(32, 32, 32);
  font-size: 14px;
  line-height: 28px;
  padding: 1px 82px 0 88px;
  text-overflow: ellipsis;
  width: 100%;
}

.search-results {
  display: block;
  width: 100%;
  z-index: 1001;
  position: absolute;
  top: 50px;
}

.search-marker {
  width: 25px;
  right: 0;
  font-size: 20px;
  line-height: 31px;
  position: absolute;
  text-align: center;
  top: 0;
  margin: 3px;
  opacity: 80%;
}

.search-select {
  font-family: inherit;
  font-size: inherit;
  border: none;
  position: absolute;
  left: 4px;
  top: 4px;
  text-align: center;
}

.search-item {
  padding-top: 5px;
  background-color: #ffffff;
  border-top: 1px #272727;
  margin-bottom: 5px;
  border-radius: 4px;
  overflow: hidden;
}

.search-date {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  color: #a0a0a0;
  display: inline;
}

.search-body {
  margin-left: 58px;
  line-height: 1.65;
  overflow: hidden;
}

.search-pic {
  margin-left: 4px;
  margin-top: 2px;
  margin-right: 10px;
  float: left;
  width: 48px;
  cursor: pointer;
}

.search-header {
  margin-bottom: 3px;
  display: block;
}

.search-title {
  margin-bottom: 3px;
  margin-top: 3px;
  font-weight: 600;
}

.search-title-date {
  height: auto;
  line-height: 1;
  margin-left: 58px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.search-content {
  cursor: pointer;
  position: relative;
  width: 100%;
  transition: margin-left 0.25s ease;
  min-height: 58px;
}
</style>