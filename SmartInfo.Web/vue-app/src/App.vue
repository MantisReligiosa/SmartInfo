<template>
    <v-card>
        <v-layout>
            <MainPanel></MainPanel>
            <v-navigation-drawer
                    color="surface"
                    class="elevation-5"
            >
                <BlocksList></BlocksList>
            </v-navigation-drawer>
            <v-navigation-drawer
                    color="surface"
                    location="right"
                    class="flex-wrap-reverse elevation-5">
                <ZoomPanel></ZoomPanel>
                <BlockProperties></BlockProperties>
            </v-navigation-drawer>
            <v-main class="vMain">
                <WorkSpace></WorkSpace>
            </v-main>
        </v-layout>
    </v-card>
</template>

<script lang="ts">
import {defineComponent} from 'vue';

import MainPanel from './components/MainPanel.vue'
import ZoomPanel from './components/ZoomPanel.vue'
import BlocksList from './components/BlocksList.vue'
import BlockProperties from './components/BlockProperties.vue'
import WorkSpace from './components/WorkSpace.vue'
import {mainStore} from './store/mainStore'
import {deviceStore} from './store/deviceStore'

export default defineComponent({
    name: 'App',
    components: {
        MainPanel,
        ZoomPanel,
        BlocksList,
        BlockProperties,
        WorkSpace
    },
    async created() {
        const mStore = mainStore()
        await mStore.load()
        await deviceStore().loadById(mStore.device?.id)
    },
})
</script>

<style>
body {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}

.vMain {
    height: 100vh;
    width: 100vw;
}
</style>
