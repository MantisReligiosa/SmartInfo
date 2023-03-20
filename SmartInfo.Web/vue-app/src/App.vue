<template>
  <v-card>
    <v-layout>
      <MainPanel></MainPanel>
      <v-navigation-drawer 
          color="surface"
      >
        <v-list>
          <v-list-item 
              title="Drawer left"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
      <v-navigation-drawer 
          color="surface" 
          location="right">
        <v-list>
          <v-list-item 
              title="Drawer right"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
      <v-main></v-main>
    </v-layout>
  </v-card>
</template>

<script>

import MainPanel from './components/MainPanel.vue'
import { mainStore } from './store/mainStore'
export default {
  name: 'App',
  components: {
    MainPanel
  },
  created() {
    const store = mainStore()
    store.load()
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
