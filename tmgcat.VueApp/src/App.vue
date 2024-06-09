<template>
  <header class="header">
    <div class="global-search">
      <label class="field">
        <select v-model="searchType" class="search-select" @change="ClearSearch()">
          <option>Игры</option>
          <option>Фильмы</option>
          <option>Сериалы</option>
        </select>
        <input placeholder="Поиск" @input="Search()" v-model="searchQuery">
        <span class="search-marker"></span>
      </label>
      <div class="search-results" cli>
        <div v-for="item in searchResults" class="search-item">
          <div class="search-content" @click="goToTitle(item.id)">
            <header class="search-header">
              <div class="search-pic">
                <img :src="getUrl(item.poster_path)" width=48px/>
              </div>
              <div class="search-title-date">
                <div class="search-title">
                  {{ item.title }}
                </div>
                <span class="search-date">
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
import formatDateMixin from '@/mixins/formatDateMixin';

export default {
  data() {
    return {
      searchResults: [],
      searchType: 'Игры',
      searchQuery: ''
    }
  },
  
  methods: {
    ClearSearch() {
      this.searchResults = [];
      this.searchQuery = ''
    },
    getUrl(link) {
            return new URL(link, import.meta.url).href;
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
    async Search() {
      console.log()
      if (this.searchQuery.length > 0) {
        try {
          switch (this.searchType) {
            case 'Игры':
              this.searchResults = (await searchGames(this.searchQuery)).titles;
              break;
            case 'Сериалы':
              //this.searchResults = (await searchTvShows(this.searchQuery).titles;
              break;
            case 'Фильмы':
              //this.searchResults = (await searchMovies(this.searchQuery).titles;
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
}

.header {
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

.global-search {
  margin-left: 300px;
  margin-right: 200px;
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
  width: 39px;
  right: 0;
  font-size: 20px;
  height: 31px;
  line-height: 31px;
  position: absolute;
  text-align: center;
  top: 0;
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