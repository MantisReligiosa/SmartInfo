<template>
    <v-container
            class="ml-0 ma-1 h-100 workSpace"
    >
        <v-card
                color="black"
                :height="h + 2 * padding"
                :width="w + 2 * padding"
                :style="{'padding-left': padding + 'px', 'padding-top': padding + 'px'}"
                class="elevation-5"
        >
            <v-card
                    :height="h"
                    :width="w"
                    :color="device.backColor"
                    class="screenCard"
            ></v-card>
        </v-card>
    </v-container>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import {computed, ref} from "vue";
import {IDevice} from "@/interfaces/IDevice";

const mStore = mainStore()
const dStore = deviceStore()

const h = computed(() => dStore.resolution.height * mStore.scale)
const w = computed(() => dStore.resolution.width * mStore.scale)

const padding = computed(() => 20 * mStore.scale)
const device = computed(() => dStore.device as IDevice)

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