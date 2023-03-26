<template>
  <v-card>
    <v-layout>
      <MainPanel></MainPanel>
      <v-navigation-drawer 
          color="surface"
      >
        <BlocksList></BlocksList>
      </v-navigation-drawer>
      <v-navigation-drawer 
          color="surface" 
          location="right">
        <ZoomPanel></ZoomPanel>
        <v-card class="h-auto">Свойста блока
          <v-footer>
          
          </v-footer>
        </v-card>
      </v-navigation-drawer>
      <v-main></v-main>
    </v-layout>
  </v-card>
</template>

<script>

import MainPanel from './components/MainPanel.vue'
import ZoomPanel from './components/ZoomPanel.vue'
import BlocksList from './components/BlocksList.vue'
import { mainStore } from './store/mainStore'
export default {
  name: 'App',
  components: {
    MainPanel,
    ZoomPanel,
    BlocksList
  },
  async created() {
    const store = mainStore()
    await store.load()
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
