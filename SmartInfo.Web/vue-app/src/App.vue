<template>
  <PreLoader />
  <MainPanel />
</template>

<script>
import MainPanel from './components/MainPanel.vue'
import PreLoader from "@/components/PreLoader.vue";

export default {
  name: 'App',
  components: {
    MainPanel,
    PreLoader
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
