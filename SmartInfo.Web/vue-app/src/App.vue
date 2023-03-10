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

<style lang="scss">
@import "~@/assets/scss/vendors/bootstrap-vue/index";
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
