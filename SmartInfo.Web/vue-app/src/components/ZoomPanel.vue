<template>
    <v-toolbar
            density="compact"
    >
        <v-spacer></v-spacer>
        <v-btn
                icon @click="zoomOut"
                size="x-small"
                elevation="1"
        >
            <v-icon>mdi-minus</v-icon>
        </v-btn>
        <v-btn
                size="small"
                @click="zoomDefault"
        >{{ scale }}
        </v-btn>
        <v-btn
                icon @click="zoomIn"
                size="x-small"
                elevation="1"
        >
            <v-icon>mdi-plus</v-icon>
        </v-btn>
        <v-spacer></v-spacer>
    </v-toolbar>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {computed} from "vue";

const mStore = mainStore()

const zoomOut = () => mStore.zoomOut()
const zoomIn = () => mStore.zoomIn()
const zoomDefault = () => mStore.zoomDefault()

const scale = computed(() => ((mStore.scale * 100) | 0) + '%')
</script>

<style scoped>

</style>