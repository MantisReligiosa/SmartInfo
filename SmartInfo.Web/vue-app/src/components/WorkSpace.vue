<template>
    <v-container class="ml-0 ma-1 h-100 workSpace">
    <v-card
        :height="h"
        :width="w"
        color="green"
        class="screenCard elevation-5"
    >
    </v-card>
    </v-container>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";

const mStore = mainStore()
const dStore = deviceStore()

const h = computed(() => dStore.resolution.height * mStore.scale)
const w = computed(() => dStore.resolution.width * mStore.scale)

</script>

<style scoped>
.workSpace {
    background-color: #eeeeee;
    max-width: 100% !important;
    overflow-x: scroll;
    overflow-y: scroll;
}

.screenCard {
    border-radius: 0;
}

</style>